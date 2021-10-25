using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] private bool m_IsInactive;
    [SerializeField] private float m_DistanceToStreet;
    [SerializeField] private float m_DistanceToSidewalk;

    public GameObject SpawnWaypoint(GameObject waypoint)
    {
        return Instantiate(waypoint, GetPosition(), Quaternion.identity);
    }

    public Vector3 GetCustomerPosition(out Quaternion rotation)
    {
        rotation = transform.rotation;
        return GetPosition(false);
    }

    private Vector3 GetPosition(bool isWaypoint = true)
    {
        return transform.rotation.eulerAngles.y switch
        {
            0f => new Vector3(transform.position.x, transform.position.y, transform.position.z + -(isWaypoint ? m_DistanceToStreet : m_DistanceToSidewalk)),
            90f => new Vector3(transform.position.x + -(isWaypoint ? m_DistanceToStreet : m_DistanceToSidewalk), transform.position.y, transform.position.z),
            180f => new Vector3(transform.position.x, transform.position.y + 0.30f, transform.position.z + (isWaypoint ? m_DistanceToStreet : m_DistanceToSidewalk)),
            270f => new Vector3(transform.position.x + (isWaypoint ? m_DistanceToStreet : m_DistanceToSidewalk), transform.position.y, transform.position.z),
            _ => new Vector3(),
        };
    }

    private void OnDrawGizmosSelected()
    {
        if (m_IsInactive) return;
        Gizmos.color = Color.white;
        Gizmos.DrawCube(GetPosition(), new Vector3(1, 1, 1));
        Gizmos.DrawSphere(GetPosition(false), 1f);
    }
}
