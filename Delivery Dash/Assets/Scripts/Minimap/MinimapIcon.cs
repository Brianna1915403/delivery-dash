using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapIcon : MonoBehaviour
{

    [SerializeField] private Transform m_MinimapCamera;
    [SerializeField] private float m_Offset = 15f;

    private Vector3 m_Position;

    private void Start()
    {
        m_MinimapCamera = GameObject.FindGameObjectWithTag("MinimapCamera")?.transform;
    }

    void Update()
    {
        m_Position = new Vector3(transform.parent.transform.position.x, transform.position.y, transform.parent.transform.position.z);
        transform.position = m_Position;
    }

    private void LateUpdate()
    {
        float x = Mathf.Clamp(transform.position.x, m_MinimapCamera.position.x - m_Offset, m_Offset + m_MinimapCamera.position.x);
        float z = Mathf.Clamp(transform.position.z, m_MinimapCamera.position.z - m_Offset, m_Offset + m_MinimapCamera.position.z);
        transform.position = new Vector3(x, transform.position.y, z);
    }
}
