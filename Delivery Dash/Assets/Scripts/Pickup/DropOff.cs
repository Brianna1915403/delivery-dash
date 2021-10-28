using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropOff : MonoBehaviour
{
    [SerializeField] private Customer m_Customer;
    [SerializeField] private CarController m_CarController;

    public Customer Customer
    {
        set { m_Customer = value; }
        get { return m_Customer; }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            m_Customer.gameObject.SetActive(true);
            m_CarController = other.transform.parent.GetComponent<CarController>();
            m_CarController.FullStop();
            Debug.Log($"Thank you!");
            m_Customer.CompleteOrder();
        }
    }
}
