using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{

    [SerializeField] private Transform m_Player;

    private void LateUpdate()
    {
        Vector3 position = new Vector3(m_Player.transform.position.x, transform.position.y, m_Player.transform.position.z);
        transform.position = position;
    }
}
