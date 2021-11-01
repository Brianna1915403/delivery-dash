using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropOff : MonoBehaviour
{
    [SerializeField] private Customer m_Customer;
    [SerializeField] private CarController m_CarController;
    [SerializeField] private Car m_Car;

    public Customer Customer
    {
        set { m_Customer = value; }
        get { return m_Customer; }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {   
            m_Car = other.transform.GetComponentInParent<Car>();
            m_Car.HasCustomer = false;
            m_Customer.gameObject.SetActive(true);
            m_Customer.CarController.ToggleFullStop();
            m_Customer.CompleteOrder();
        }
    }
}
