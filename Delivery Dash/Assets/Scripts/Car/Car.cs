using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField] private ParticleSystem m_Smoke;
    private ParticleSystem.EmissionModule m_Emission;
    [SerializeField] private float m_Damage = 0f;

    [Header("Customer Satisfaction")]
    [SerializeField] private float m_Rating = 5f;
    [Space]
    [SerializeField] private bool m_HasCustomer = false;
    [SerializeField] private float m_CrashPenalty = 0.5f;
    [SerializeField] private float m_SurfacePenalty = 0.1f;

    public bool HasCustomer
    {
        get { return m_HasCustomer; }
        set { m_HasCustomer = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        m_Emission = m_Smoke.emission;
    }

    // Update is called once per frame
    void Update()
    {
        CheckDamage();
    }

    private void CheckDamage()
    {
        if (m_Damage > 0)
        {
            m_Smoke.Play();            
            m_Emission.rateOverTime = m_Damage * 10f;            
        }
        else
        {
            m_Smoke.Stop();
        }
    }

    public void TakeDamage(float amount)
    {
        m_Damage += amount;
        m_Rating -= HasCustomer ? m_CrashPenalty : 0f;
    }

    public void UpdatePenalty(float time)
    {
        m_Rating -= HasCustomer ? m_SurfacePenalty * (time / 10f) : 0f;
    }
}
