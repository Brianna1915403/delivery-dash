using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataHandler : MonoBehaviour
{
    [Header("Save Data")]
    [SerializeField] private bool m_HasSeenIntro = false;
    [SerializeField] private float m_Debt;
    [SerializeField] private int m_Day;
    [SerializeField] private float m_AverageRating;
    [SerializeField] private int m_CustomersServed;

    public bool HasSeenIntro
    {
        get { return m_HasSeenIntro; }
        set { m_HasSeenIntro = value; }
    }

    public void Save()
    {
        // No Time
    }
}
