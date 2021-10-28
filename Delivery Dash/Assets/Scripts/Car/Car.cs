using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField] private ParticleSystem m_Smoke;
    private ParticleSystem.EmissionModule m_Emission;
    private ParticleSystem.Burst m_Burst;
    [SerializeField] private float m_Damage = 0f;
    [SerializeField] private float m_Penalty;

    // Start is called before the first frame update
    void Start()
    {
        m_Emission = m_Smoke.emission;
        m_Burst = m_Emission.GetBurst(0);
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
    }

    public void TakeDamage(float amount)
    {
        m_Damage += amount;
    }
}
