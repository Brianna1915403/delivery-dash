using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Customer : MonoBehaviour
{
    [Header("Taxi Order")]
    [SerializeField] private WaypointRadius m_PickupWaypoint;
    [SerializeField] private WaypointRadius m_DropOffWaypoint;

    [Header("Person")]
    [SerializeField] private NavMeshAgent m_Agent;
    [SerializeField] private Animator m_Animator;
    [SerializeField] private BoxCollider m_Trigger;

    private bool m_IsBeingPickedUp;
    private bool m_IsBeingDroppedOff;

    private float m_Step = 0;
    private int m_Offset = 2;

    private Transform m_Car;
    private CarController m_CarController;
    private Transform m_Building;

    public CarController CarController {
        set { m_CarController = value; }
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

    // --- OVERRIDES START ---

    public void Start()
    {
        Idle();       
    }

    public void FixedUpdate()
    {
        m_Step = 2f * Time.deltaTime;
        if (m_IsBeingPickedUp)
        {
            m_Agent.SetDestination(m_Car.position);
            //transform.position = Vector3.Lerp(transform.position, m_Car.position, m_Step);
        } 
        else if (m_IsBeingDroppedOff)
        {
            m_Car.gameObject.SetActive(false);
            m_Building = m_DropOffWaypoint.Building.gameObject.transform;
            Debug.Log($"Customer Position: {transform.position} | Building : {m_Building.name} | Building Position: {m_Building.position}");
            transform.position = Vector3.Lerp(GetDropOffPosition(), m_Building.position, m_Step);
            transform.eulerAngles = TranslateRotation(m_Building.rotation, true);
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
            Invoke("CleanUpOrder", 1f);                       
            m_CarController.Restart();            
        } 
        else if (other.CompareTag("Player"))
        {
            CleanUpOrder();
        }
    }

    // --- OVERRIDES END ---

    // --- ORDER START ---

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
    
    public void CompleteOrder()
    {
        Walk();
        m_IsBeingDroppedOff = true;
    }

    private void CleanUpOrder()
    {
        Destroy(m_PickupWaypoint.Waypoint.gameObject);
        Destroy(m_DropOffWaypoint.Waypoint.gameObject);
        Destroy(gameObject);
    }

    // --- ORDER END ---

    // --- ANIMATION START ---

    public void GetInCab(Transform car, CarController carController)
    {
        Debug.Log("Orphans in my basement");
        Walk();
        m_Car = car;
        m_CarController = carController;
        m_IsBeingPickedUp = true;
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

    // --- ANIMATION END ---

    // --- POSITIONING START ---

    private Vector3 GetDropOffPosition()
    {
        return m_Building.rotation.eulerAngles.y switch
        {
            0f => new Vector3(m_Car.transform.position.x, m_Car.transform.position.y, m_Car.transform.position.z + m_Offset),
            90f => new Vector3(m_Car.transform.position.x + m_Offset, m_Car.transform.position.y, m_Car.transform.position.z),
            180f => new Vector3(m_Car.transform.position.x, m_Car.transform.position.y, m_Car.transform.position.z - m_Offset),
            270f => new Vector3(m_Car.transform.position.x - m_Offset, m_Car.transform.position.y, m_Car.transform.position.z),
            _ => Vector3.zero,
        };
    }

    private Vector3 TranslateRotation(Quaternion quaternion, bool alter = false)
    {
        if (alter) 
        {
            return quaternion.eulerAngles.y switch
            {
                0f => new Vector3(0f, 90f, 0f),
                90f => new Vector3(0f, 180f, 0f),
                180f => new Vector3(0f, 270f, 0f),
                270f => new Vector3(0f, 0f, 0f),
                _ => Vector3.zero,
            };
        }
        else
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

    // --- POSITIONING END ---
}
