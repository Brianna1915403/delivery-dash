using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropOff : MonoBehaviour
{
    public Transform pickUp;

    private void OnTriggerEnter(Collider other)
    {
        if (pickUp == null) return;

        if (other.CompareTag("Player") && !pickUp.gameObject.activeSelf)
        {
            Debug.Log($"Thank you for my order!");
            Destroy(pickUp.gameObject);
            Destroy(this.gameObject);
        }
    }
}
