using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField] private ParticleSystem m_Smoke;
    private ParticleSystem.EmissionModule m_Emission;
    [SerializeField] private float m_Damage = 0f;
    [SerializeField] private float m_CrashDamage = 0.1f;

    [Header("Customer")]
    [SerializeField] private bool m_HasCustomer = false;
    

    public bool HasCustomer
    {
        get { return m_HasCustomer; }
        set { m_HasCustomer = value; }
    }

    public float Damage
    {
        get { return m_Damage; }
    }

    void Start()
    {
        m_Emission = m_Smoke.emission;
    }

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

    public void TakeCrashDamage()
    {
        m_Damage += m_CrashDamage;
        GameManager.Instance.ScoreHandler.Rating -= HasCustomer ? GameManager.Instance.ScoreHandler.CrashPenalty : 0f;
    }

    public void UpdateSurfacePenalty(float time)
    {
        GameManager.Instance.ScoreHandler.Rating -= HasCustomer ? GameManager.Instance.ScoreHandler.SurfacePenalty * (time / 10f) : 0f;
    }
}
