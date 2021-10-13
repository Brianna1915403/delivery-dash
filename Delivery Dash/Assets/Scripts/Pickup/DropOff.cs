using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropOff : MonoBehaviour
{
    [SerializeField] private Customer m_Customer;

    public Customer Customer
    {
        set { m_Customer = value; }
        get { return m_Customer; }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log($"Thank you!");
            m_Customer.CompleteOrder();
        }
    }
}
