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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        m_CurrentTime = Time.time;
    }

    private void OnTriggerEnter(Collider other) {
        if (!m_IsWheel && other.CompareTag("Building") && m_CurrentTime >= m_TargetTime) {
            m_TargetTime = Time.time + m_TriggerDelay;
            m_Car.TakeDamage(0.1f);
            Debug.Log($"Target: {m_TargetTime} | Current: {m_CurrentTime}");
        }
        else if (m_IsWheel && other.CompareTag("Sidewalk"))
        {
            Debug.Log("Trigger Enter...");
            m_PenaltyStartTime = Time.deltaTime;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (m_IsWheel && other.CompareTag("Sidewalk"))
        {
            Debug.Log("Trigger Exit");
            m_Car.UpdatePenalty(m_CurrentTime - m_PenaltyStartTime);
        }
    }

    private void Respawn() {
        m_CarController.Brake(true);
        transform.rotation = new Quaternion(0, 0, 0, 1);
        transform.position = new Vector3(30, 2, 61);
    }
}
