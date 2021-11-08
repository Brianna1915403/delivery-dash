using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderHandeler : MonoBehaviour
{
    [SerializeField] private WaypointRadius[] m_PickupLocations;
    [SerializeField] private WaypointRadius[] m_DropOffLocations;
    [Space]
    [SerializeField] private GameObject m_CustomerPrefab;

    void Awake()
    {
        m_PickupLocations = transform.GetChild(0).GetComponentsInChildren<WaypointRadius>();
        m_DropOffLocations = transform.GetChild(1).GetComponentsInChildren<WaypointRadius>();
    }

    public bool SpawnCustomer()
    {
        ActivateWaypoints();
        WaypointRadius pickup = m_PickupLocations[Random.Range(0, m_PickupLocations.Length)];
        WaypointRadius dropoff = m_DropOffLocations[Random.Range(0, m_DropOffLocations.Length)];
        
        GameObject customerObj = Instantiate(m_CustomerPrefab, Vector3.zero, Quaternion.identity);
        if (customerObj) {
            Customer customer = customerObj.GetComponent<Customer>();
            customer.DropOffWaypoint = dropoff;
            customer.PickupWaypoint = pickup;
            customer.OrderPickup();
            return true;
        }
        return false;
    }

    private void ActivateWaypoints()
    {
        foreach (WaypointRadius radius in m_PickupLocations)
        {
            if (radius.Buildings == null || radius.Buildings.Count == 0) 
                radius.GetBuildings();
        }
        foreach (WaypointRadius radius in m_DropOffLocations)
        {
            if (radius.Buildings == null || radius.Buildings.Count == 0)
                radius.GetBuildings();
        }
    }

    public void ClearWaypoints()
    {
        foreach (WaypointRadius radius in m_PickupLocations)
        {
            radius.Buildings.Clear();
        }
        foreach (WaypointRadius radius in m_DropOffLocations)
        {
            radius.Buildings.Clear();
        }
    }
}
