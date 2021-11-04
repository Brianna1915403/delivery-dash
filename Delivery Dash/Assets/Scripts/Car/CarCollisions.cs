using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCollisions : MonoBehaviour
{
    [Header("Delay")]
    [SerializeField] private float m_TriggerDelay;
    private float m_CurrentTime;
    private float m_TargetTime;

    [Header("Car Components")]
    [SerializeField] private CarController m_CarController;
    [SerializeField] private Car m_Car;
    [SerializeField] private bool m_IsWheel;

    private float m_PenaltyStartTime;

    void Update()
    {
        m_CurrentTime = Time.time;
    }

    private void OnTriggerEnter(Collider other) {
        if (!m_IsWheel && other.CompareTag("Building") && m_CurrentTime >= m_TargetTime) 
        {
            m_TargetTime = Time.time + m_TriggerDelay;
            m_Car.TakeCrashDamage();
        }
        else if (m_IsWheel && other.CompareTag("Sidewalk"))
        {
            m_PenaltyStartTime = Time.deltaTime;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (m_IsWheel && other.CompareTag("Sidewalk"))
        {
            m_Car.UpdateSurfacePenalty(m_CurrentTime - m_PenaltyStartTime);
        }
    }
}
