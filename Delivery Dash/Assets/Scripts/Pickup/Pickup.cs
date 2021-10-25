using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
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
            m_Customer.DirectToDropOff();
            gameObject.SetActive(false);
        }
    }
}
