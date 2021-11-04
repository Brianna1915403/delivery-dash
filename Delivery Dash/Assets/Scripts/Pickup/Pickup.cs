using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] private Customer m_Customer;
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
            Customer.CarController = other.transform.GetComponentInParent<CarController>();
            Customer.CarController.ToggleFullStop();
            m_Car = other.transform.GetComponentInParent<Car>();
            m_Car.HasCustomer = true;
            m_Customer.GetInCab(other.transform, Customer.CarController);
            m_Customer.FirstImpression(m_Car.Damage > 0f);
            gameObject.SetActive(false);
        }
    }
}
