using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] private Transform[] m_Wheels;  
    [SerializeField] private WheelCollider[] m_WheelColliders;
    [Space]
    [SerializeField] private float m_Motor = 500f;
    [SerializeField] private float m_Brake = 400f;
    [SerializeField] private float m_SteeringAngle = 30f;

    private float m_verticalInput;
    private float m_horizontalInput;
    private float m_steeringAngle;

    void FixedUpdate()
    {       
        Drive();
    }

    void Drive()
    {
        m_verticalInput = Input.GetAxis("Vertical");
        m_horizontalInput = Input.GetAxis("Horizontal");
        Brake(Input.GetButton("Jump"));

        Steer();
        Accelerate();
        UpdateWheelPlacement();
    }

    void Steer() 
    {
        m_steeringAngle = m_SteeringAngle * m_horizontalInput;
        m_WheelColliders[0].steerAngle = m_steeringAngle;
        m_WheelColliders[1].steerAngle = m_steeringAngle;
    }

    void Accelerate()
    {
        m_WheelColliders[0].motorTorque = m_verticalInput * m_Motor;
        m_WheelColliders[1].motorTorque = m_verticalInput * m_Motor;
    }

    void Brake(bool isActive = true)
    {
        m_WheelColliders[0].brakeTorque = isActive ? m_Brake : 0f;
        m_WheelColliders[1].brakeTorque = isActive ? m_Brake : 0f;
        m_WheelColliders[2].brakeTorque = isActive ? m_Brake : 0f;
        m_WheelColliders[3].brakeTorque = isActive ? m_Brake : 0f;
    }

    void UpdateWheelPlacement()
    {
        UpdateWheelPlacement(m_Wheels[0], m_WheelColliders[0]);
        UpdateWheelPlacement(m_Wheels[1], m_WheelColliders[1]);
        UpdateWheelPlacement(m_Wheels[2], m_WheelColliders[2]);
        UpdateWheelPlacement(m_Wheels[3], m_WheelColliders[3]);
    }

    void UpdateWheelPlacement(Transform transform, WheelCollider collider)
    {
        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);
        transform.position = position;
        transform.rotation = rotation;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, Vector3.forward);
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector3.right); 
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, Vector3.up);
    }
}
