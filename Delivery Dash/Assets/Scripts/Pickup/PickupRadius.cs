using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupRadius : MonoBehaviour
{
    [SerializeField] private GameObject m_PickupPrefab;
    [SerializeField] private GameObject m_DropOffPrefab;
    [Space]
    [SerializeField] private float m_Offset = 10f;
    [Space]
    [SerializeField] private float m_Radius = 5f;
    [SerializeField] private List<GameObject> buildings;

    private Pickup m_Pickup;
    private DropOff m_DropOff;

    private void Start()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, m_Radius);
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.CompareTag("Building"))
                buildings.Add(collider.gameObject);
        }
        CreatePickup();
    }

    private void CreatePickup()
    {
        int random = Random.Range(0, buildings.Count);
        GameObject building = buildings[random];
        Debug.Log($"START: {building.name}");
        Vector3 position = GetSpawnLocation(building.transform);
        Debug.Log($"START: {position}");
        GameObject pickup = Instantiate(m_PickupPrefab, position, Quaternion.identity);
        m_Pickup = pickup.GetComponent<Pickup>();
        m_Pickup.m_DropOff = null;
    }

    private Vector3 GetSpawnLocation(Transform transform)
    {
        Debug.Log($"GET_SPAWN_LOCATION: {transform.rotation.eulerAngles}");
        Debug.Log($"GET_SPAWN_LOCATION: {transform.transform.position}");
        return transform.rotation.eulerAngles.y switch
        {
            0f => new Vector3(transform.position.x, transform.position.y + 0.30f, transform.position.z + -m_Offset),
            90f => new Vector3(transform.position.x + -m_Offset, transform.position.y + 0.30f, transform.position.z),
            180f => new Vector3(transform.position.x, transform.position.y + 0.30f, transform.position.z + m_Offset),
            270f => new Vector3(transform.position.x + m_Offset, transform.position.y + 0.30f, transform.position.z),
            _ => new Vector3(),
        };
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, m_Radius);
    }

}
