using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropOff : MonoBehaviour
{
    public Transform order;

    private void OnTriggerEnter(Collider other)
    {
        if (order == null) return;

        if (other.CompareTag("Player") && !order.gameObject.activeSelf)
        {
            Debug.Log($"Thank you for my order!");
            Destroy(order.gameObject);
            Destroy(this.gameObject);
        }
    }
}
