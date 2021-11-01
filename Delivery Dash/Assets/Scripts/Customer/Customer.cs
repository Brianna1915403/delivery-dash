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
    [SerializeField] private Vector3 m_Target;
    [SerializeField] private float m_Distance;
    [SerializeField] private float m_StoppingDistance = 0.25f;
    [Space]
    [SerializeField] private Animator m_Animator;

    private Transform m_Building;
    private float m_Offset = 2f;

    private bool m_IsBeingPickedUp;
    private bool m_IsBeingDroppedOff;

    private Transform m_Car;
    [SerializeField] private CarController m_CarController;

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
        if (m_IsBeingPickedUp)
        {            
            m_Target = m_Car.position;
            m_Agent.SetDestination(m_Target);
            m_Distance = Vector3.Distance(transform.position, m_Target);
            if (m_Distance <= m_StoppingDistance) {
                Debug.Log("Pick Up!");
                GetPickedUp();
            }
        } 
        else if (m_IsBeingDroppedOff)
        {                       
            m_Target = m_Building.position;
            m_Agent.SetDestination(m_Target);
            m_Distance = Vector3.Distance(transform.position, m_Target);
            if (m_Distance <= m_StoppingDistance) {
                CleanUpOrder();
            }
        }            
    }

    private void OnDestroy() {
        m_CarController.ToggleFullStop();
    }

    // --- OVERRIDES END ---

    // --- ORDER START ---

    public void OrderPickup()
    {
        m_PickupWaypoint.SpawnWaypoint();

        gameObject.transform.position = m_PickupWaypoint.Building.GetCustomerPosition(out Quaternion rotation);
        gameObject.transform.rotation = rotation;

        m_PickupWaypoint.Waypoint.GetComponent<Pickup>().Customer = this;
    }

    private void GetPickedUp() 
    {
        DirectToDropOff();
        m_CarController.ToggleFullStop();
        m_IsBeingPickedUp = false;
        gameObject.SetActive(false);
    }

    public void DirectToDropOff()
    {
        m_DropOffWaypoint.SpawnWaypoint();
        m_Building = m_DropOffWaypoint.Building.gameObject.transform;
        m_DropOffWaypoint.Waypoint.GetComponent<DropOff>().Customer = this;
    }    
    
    public void CompleteOrder()
    {
        Walk();
        m_IsBeingDroppedOff = true; 
        transform.position = GetDropOffPosition();
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

    private Vector3 GetDropOffPosition() {
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
