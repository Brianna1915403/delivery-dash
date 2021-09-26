using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order : MonoBehaviour
{
    public Transform dropOff;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log($"Please Drop off to {dropOff.name} at {dropOff.position}");
            gameObject.SetActive(false);
        }
    }
}
