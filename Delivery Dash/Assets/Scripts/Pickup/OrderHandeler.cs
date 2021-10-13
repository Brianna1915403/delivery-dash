using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderHandeler : MonoBehaviour
{
    [SerializeField] private WaypointRadius[] m_PickupLocations;
    [SerializeField] private WaypointRadius[] m_DropOffLocations;
    [Space]
    [SerializeField] private GameObject m_CustomerPrefab;

    void Start()
    {
        m_PickupLocations = transform.GetChild(0).GetComponentsInChildren<WaypointRadius>();
        m_DropOffLocations = transform.GetChild(1).GetComponentsInChildren<WaypointRadius>();
        SpawnCustomer();
    }

    public void SpawnCustomer()
    {
        WaypointRadius pickup = m_PickupLocations[Random.Range(0, m_PickupLocations.Length)];
        WaypointRadius dropoff = m_DropOffLocations[Random.Range(0, m_DropOffLocations.Length)];
        
        GameObject customerObj = Instantiate(m_CustomerPrefab, Vector3.zero, Quaternion.identity);
        Customer customer = customerObj.GetComponent<Customer>();
        customer.DropOffWaypoint = dropoff;
        customer.PickupWaypoint = pickup;

        customer.OrderPickup();
    }
}
