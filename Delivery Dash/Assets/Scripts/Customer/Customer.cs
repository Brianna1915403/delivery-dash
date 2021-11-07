using System;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour 
{
    [Header("Taxi Order")]
    [SerializeField] private WaypointRadius m_PickupWaypoint;
    [SerializeField] private WaypointRadius m_DropOffWaypoint;

    [Header("Person")]
    [SerializeField] private Vector3 m_Target;
    [SerializeField] private float m_Distance;
    [Space]
    [SerializeField] private Animator m_Animator;

    private bool m_IsBeingPickedUp;
    private bool m_IsBeingDroppedOff;
    private bool m_HasBeenHit;

    private int m_Offset = 2;
    [SerializeField] TimeSpan m_WaitTime;

    private Transform m_Car;
    private CarController m_CarController;
    private Transform m_Building;

    public CarController CarController 
    {
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

    public void Start() {
        Idle();
        m_Distance = Vector3.Distance(transform.position, m_Target);
        m_WaitTime = GameManager.Instance.TimeHandler.CurrentTime.TimeOfDay + TimeSpan.FromMinutes(m_Distance);        
        Debug.Log($"Wait Time: {m_WaitTime}");
    }

    public void FixedUpdate() {
        if (m_IsBeingPickedUp) 
        {
            m_Target = m_Car.position;
            transform.position = Vector3.MoveTowards(transform.position, m_Target, 0.1f);
        } 
        else if (m_IsBeingDroppedOff) 
        {
            m_Target = m_Building.position;            
            transform.position = Vector3.MoveTowards(transform.position, m_Target, 0.1f);            
        } 
        else if (GameManager.Instance.TimeHandler.CurrentTime.TimeOfDay >= m_WaitTime)
        {
            CancelOrder();
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (m_IsBeingPickedUp && collision.collider.CompareTag("Player"))
        {
            GetPickedUp();
        }
        else if (!m_IsBeingPickedUp && collision.collider.CompareTag("Player"))
        {
            m_HasBeenHit = true;
            CancelOrder();
        }
        if (m_IsBeingDroppedOff && collision.collider.CompareTag("Building"))
        {
            m_CarController.ToggleFullStop();
            CleanUpOrder();
        }
    }

    // --- OVERRIDES END ---

    // --- ORDER START ---

    public void FirstImpression(bool isCarDamaged) {
        GameManager.Instance.ScoreHandler.Rating -= isCarDamaged ? GameManager.Instance.ScoreHandler.FirstImpressionPenalty : 0f;
    }

    public void OrderPickup() {
        m_PickupWaypoint.SpawnWaypoint();

        gameObject.transform.position = m_PickupWaypoint.Building.GetCustomerPosition(out Quaternion rotation);
        gameObject.transform.eulerAngles = TranslateRotation(rotation, true);

        m_PickupWaypoint.Waypoint.GetComponent<Pickup>().Customer = this;
    }

    private void GetPickedUp() {
        DirectToDropOff();
        m_CarController.ToggleFullStop();
        m_IsBeingPickedUp = false;
        gameObject.SetActive(false);
    }

    public void DirectToDropOff() {
        m_DropOffWaypoint.SpawnWaypoint();
        m_DropOffWaypoint.Waypoint.GetComponent<DropOff>().Customer = this;
        m_Building = m_DropOffWaypoint.Building.gameObject.transform;
        gameObject.transform.eulerAngles = TranslateRotation(m_Building.rotation);
    }

    public void CompleteOrder() {
        Walk();
        m_IsBeingDroppedOff = true;
        transform.position = GetDropOffPosition();
    }

    private void CleanUpOrder() {        
        Destroy(m_PickupWaypoint.Waypoint.gameObject);
        Destroy(m_DropOffWaypoint.Waypoint.gameObject);
        if (!m_HasBeenHit) GameManager.Instance.ScoreHandler.NextClient();
        GameManager.Instance.IsOccupied = false;
        Destroy(this.gameObject);
    }

    private void CancelOrder()
    {
        if (m_HasBeenHit)
        {
            GameManager.Instance.ScoreHandler.Rating = 1;
            Invoke("CleanUpOrder", 1f);
        } else
        {
            CleanUpOrder();
        }
    }

    // --- ORDER END ---

    // --- ANIMATION START ---

    public void GetInCab(Transform car, CarController carController) {
        Walk();
        m_Car = car;
        m_CarController = carController;
        m_IsBeingPickedUp = true;
    }

    private void Idle() {
        m_Animator.SetInteger("arms", 5);
        m_Animator.SetInteger("legs", 5);
    }

    private void Walk() {
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

    private Vector3 TranslateRotation(Quaternion quaternion, bool isFacingAwayBuilding = false) {
        if (isFacingAwayBuilding) {
            return quaternion.eulerAngles.y switch
            {
                0f => new Vector3(0f, 270f, 0f),
                90f => new Vector3(0f, 0f, 0f),
                180f => new Vector3(0f, 90f, 0f),
                270f => new Vector3(0f, 180f, 0f),
                _ => Vector3.zero,
            };
        } else {
            return quaternion.eulerAngles.y switch
            {
                0f => new Vector3(0f, 90f, 0f),
                90f => new Vector3(0f, 180f, 0f),
                180f => new Vector3(0f, 270f, 0f),
                270f => new Vector3(0f, 0f, 0f),
                _ => Vector3.zero,
            };
        }

    }

    // --- POSITIONING END ---
}
