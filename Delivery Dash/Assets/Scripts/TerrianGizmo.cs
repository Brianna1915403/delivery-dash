using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrianGizmo : MonoBehaviour
{
    [SerializeField] private Vector3 m_Size;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(transform.position, m_Size);
    }
}
