using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RADIUS_TYPE { PICKUP, DROP_OFF }

public class WaypointRadius : MonoBehaviour
{
    [SerializeField] private RADIUS_TYPE m_RadiusType;
    [SerializeField] private GameObject m_WaypointPrefab;
    [Space]
    [SerializeField] private float m_Radius = 5f;
    [SerializeField] private List<GameObject> m_Buildings;
    [SerializeField] private Building m_Building;
    [SerializeField] private GameObject m_Waypoint;

    public Building Building
    {
        get { return m_Building; }
    }

    public GameObject Waypoint
    {
        get { return m_Waypoint; }
    }

    private void Start() {
        Collider[] colliders = Physics.OverlapSphere(transform.position, m_Radius);
        foreach (Collider collider in colliders) {
            if (collider.gameObject.CompareTag("Building"))
                m_Buildings.Add(collider.gameObject);
        }
    }

    public void SpawnWaypoint()
    {
        m_Building = m_Buildings[Random.Range(0, m_Buildings.Count)]?.GetComponent<Building>();
        m_Waypoint = m_Building.SpawnWaypoint(m_WaypointPrefab);
    }

    private void OnDrawGizmos() {
        Gizmos.color = m_RadiusType.Equals(RADIUS_TYPE.PICKUP)? Color.green : Color.red;
        Gizmos.DrawWireSphere(transform.position, m_Radius);
    }
}
