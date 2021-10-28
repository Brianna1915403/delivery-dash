using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    [Header("Taxi Order")]
    [SerializeField] private WaypointRadius m_PickupWaypoint;
    [SerializeField] private WaypointRadius m_DropOffWaypoint;

    [Header("Person")]
    [SerializeField] private Animator m_Animator;
    [SerializeField] private BoxCollider m_Trigger;

    private bool m_IsBeingPickedUp;
    private bool m_IsBeingDroppedOff;
    private Transform m_Car;
    private CarController m_CarController;

    public CarController CarController {
        get { return m_CarController; }
    }

    public WaypointRadius PickupWaypoint
    {
        set { m_PickupWaypoint = value; }
        get { return m_PickupWaypoint; }
    }

    public WaypointRadius DropOffWaypoint
    {
        set { m_DropOffWaypoint = value; }
        get { return m_DropOffWaypoint; }
    }

    public void Start()
    {
        Idle();
    }

    public void Update()
    {
        if (m_IsBeingPickedUp)
        {
            transform.position = Vector3.MoveTowards(transform.position, m_Car.position, 0.1f);
        } 
        else if (m_IsBeingDroppedOff)
        {
            m_Car.gameObject.SetActive(false);
            transform.position = Vector3.MoveTowards(m_Car.position, m_DropOffWaypoint.Building.transform.position, 0.1f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && m_IsBeingPickedUp)
        {
            DirectToDropOff();
            m_CarController.Restart();
            m_IsBeingPickedUp = false;
            gameObject.SetActive(false);
        }
        else if (other.CompareTag("Building"))
        {
            
            m_CarController.Restart();
            Destroy(gameObject);
        }
    }

    public void OrderPickup()
    {
        m_PickupWaypoint.SpawnWaypoint();

        gameObject.transform.position = m_PickupWaypoint.Building.GetCustomerPosition(out Quaternion rotation);
        gameObject.transform.eulerAngles = TranslateRotation(rotation);

        m_PickupWaypoint.Waypoint.GetComponent<Pickup>().Customer = this;
    }

    public void DirectToDropOff()
    {
        m_DropOffWaypoint.SpawnWaypoint();
        m_DropOffWaypoint.Waypoint.GetComponent<DropOff>().Customer = this;
    }

    public void GetInCab(Transform car, CarController carController) {
        Debug.Log("Orphans in my basement");
        Walk();
        m_Car = car;
        m_CarController = carController;        
        m_IsBeingPickedUp = true;
    }
    
    public void CompleteOrder()
    {
        Walk();
        m_IsBeingDroppedOff = true;
        Invoke("CleanUpOrder", 2f);
    }

    private void CleanUpOrder()
    {
        m_Car.gameObject.SetActive(true);
        Destroy(m_PickupWaypoint.Waypoint.gameObject);
        Destroy(m_DropOffWaypoint.Waypoint.gameObject);
    }

    private void Idle()
    {
        m_Animator.SetInteger("arms", 5);
        m_Animator.SetInteger("legs", 5);
    }

    private void Wave() {
        m_Animator.SetInteger("arms", 16);
        m_Animator.SetInteger("legs", 16);
    }

    private void Walk()
    {
        m_Animator.SetInteger("arms", 1);
        m_Animator.SetInteger("legs", 1);
    }

    private Vector3 TranslateRotation(Quaternion quaternion)
    {
        return quaternion.eulerAngles.y switch
        {
            0f => new Vector3(0f, 270f, 0f),
            90f => new Vector3(0f, 0f, 0f),
            180f => new Vector3(0f, 90f, 0f),
            270f => new Vector3(0f, 180f, 0f),
            _ => Vector3.zero,
        };
    }
}
