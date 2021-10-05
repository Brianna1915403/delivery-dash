using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointRadius : MonoBehaviour
{
    [SerializeField] private string m_RadiusType;
    [Space]
    [SerializeField] private GameObject m_PickupPrefab;
    [SerializeField] private GameObject m_DropOffPrefab;
    [Space]
    [SerializeField] private float m_Radius = 5f;
    [SerializeField] private List<GameObject> buildings;

    private Pickup m_Pickup;
    private DropOff m_DropOff;

    private void Start() {
        Collider[] colliders = Physics.OverlapSphere(transform.position, m_Radius);
        foreach (Collider collider in colliders) {
            if (collider.gameObject.CompareTag("Building"))
                buildings.Add(collider.gameObject);
        }
        // Choose building and make it spawn the thing...
    }



    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, m_Radius);
    }
}
