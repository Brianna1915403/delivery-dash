using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] private Transform m_DropOff;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log($"Please Drop off to {m_DropOff.name} at {m_DropOff.position}");
            gameObject.SetActive(false);
        }
    }
}
