using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] private float m_DistanceToStreet;

    public void SpawnWaypoint(GameObject waypoint)
    {
        Instantiate(waypoint, GetPosition(), Quaternion.identity);
    }

    private Vector3 GetPosition()
    {
        return transform.rotation.eulerAngles.y switch
        {
            0f => new Vector3(transform.position.x, transform.position.y, transform.position.z + -m_DistanceToStreet),
            90f => new Vector3(transform.position.x + -m_DistanceToStreet, transform.position.y, transform.position.z),
            180f => new Vector3(transform.position.x, transform.position.y + 0.30f, transform.position.z + m_DistanceToStreet),
            270f => new Vector3(transform.position.x + m_DistanceToStreet, transform.position.y, transform.position.z),
            _ => new Vector3(),
        };
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawCube(GetPosition(), new Vector3(1, 1, 1));
    }
}
