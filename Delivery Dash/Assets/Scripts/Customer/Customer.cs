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
        Wave();
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

    public void CompleteOrder()
    {
        Destroy(m_PickupWaypoint.Waypoint.gameObject);
        Destroy(m_DropOffWaypoint.Waypoint.gameObject);
    }

    public void GetInCab(Transform car) {
        Debug.Log("Orphans in my basement");
        
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
