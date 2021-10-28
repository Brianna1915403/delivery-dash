using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
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
            m_CarController = other.transform.parent.GetComponent<CarController>();
            m_CarController.FullStop();
            m_Customer.GetInCab(other.transform, m_CarController);
            gameObject.SetActive(false);
        }
    }
}
