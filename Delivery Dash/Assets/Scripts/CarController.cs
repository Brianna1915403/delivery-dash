using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public Transform[] wheels;  
    public WheelCollider[] wheelColliders;
    [Space]
    public float motor = 50f;
    public float brake = 400f;
    public float steeringAngle = 30f;

    private float m_verticalInput;
    private float m_horizontalInput;
    private float m_steeringAngle;

    private void Start() 
    {
        
    }

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
        m_steeringAngle = steeringAngle * m_horizontalInput;
        wheelColliders[0].steerAngle = m_steeringAngle;
        wheelColliders[1].steerAngle = m_steeringAngle;
    }

    void Accelerate()
    {
        wheelColliders[0].motorTorque = m_verticalInput * motor;
        wheelColliders[1].motorTorque = m_verticalInput * motor;
    }

    void Brake(bool isActive = true)
    {
        wheelColliders[0].brakeTorque = isActive ? brake : 0f;
        wheelColliders[1].brakeTorque = isActive ? brake : 0f;
    }

    void UpdateWheelPlacement()
    {
        UpdateWheelPlacement(wheels[0], wheelColliders[0]);
        UpdateWheelPlacement(wheels[1], wheelColliders[1]);
        UpdateWheelPlacement(wheels[2], wheelColliders[2]);
        UpdateWheelPlacement(wheels[3], wheelColliders[3]);
    }

    void UpdateWheelPlacement(Transform transform, WheelCollider collider)
    {
        Vector3 position = transform.position;
        Quaternion rotation = transform.rotation;
        collider.GetWorldPose(out position, out rotation);
        transform.position = position;
        transform.rotation = rotation;
    }
}
