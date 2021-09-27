using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupRadius : MonoBehaviour
{
    public GameObject pickupPrefab;
    public GameObject dropOffPrefab;
    [Space]
    public float offset = 10f;
    [Space]
    public float radius = 5f;
    public List<GameObject> buildings;

    private Pickup m_pickup;
    private DropOff m_dropOff;

    private void Start()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
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
        GameObject pickup = Instantiate(pickupPrefab, position, Quaternion.identity);
        m_pickup = pickup.GetComponent<Pickup>();
        m_pickup.dropOff = null;
    }

    private Vector3 GetSpawnLocation(Transform transform)
    {
        Debug.Log($"GET_SPAWN_LOCATION: {transform.rotation.eulerAngles}");
        Debug.Log($"GET_SPAWN_LOCATION: {transform.transform.position}");
        return transform.rotation.eulerAngles.y switch
        {
            0f => new Vector3(transform.position.x, transform.position.y + 0.30f, transform.position.z + -offset),
            90f => new Vector3(transform.position.x + -offset, transform.position.y + 0.30f, transform.position.z),
            180f => new Vector3(transform.position.x, transform.position.y + 0.30f, transform.position.z + offset),
            270f => new Vector3(transform.position.x + offset, transform.position.y + 0.30f, transform.position.z),
            _ => new Vector3(),
        };
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

}
