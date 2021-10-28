using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    //[SerializeField] private bool m_HasActiveControls = true;
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
        if (Input.GetKeyDown(KeyCode.F))
            Flip();
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

    public void Brake(bool isActive = true)
    {
        if (isActive) {
            m_WheelColliders[0].brakeTorque = m_Brake;
            m_WheelColliders[1].brakeTorque = m_Brake;
            m_WheelColliders[2].brakeTorque = m_Brake;
            m_WheelColliders[3].brakeTorque = m_Brake;
        }
        else 
        {
            m_WheelColliders[0].brakeTorque = 0f;
            m_WheelColliders[1].brakeTorque = 0f;
            m_WheelColliders[2].brakeTorque = 0f;
            m_WheelColliders[3].brakeTorque = 0f;
        }
    }

    void Flip() {
        Debug.Log("Orphans in my basement");
        if (transform.rotation.eulerAngles.x > 1 && transform.rotation.eulerAngles.z > 1)
            transform.rotation = new Quaternion(0, transform.rotation.y, 0, 1);
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

    public void FullStop()
    {
        GetComponent<Rigidbody>().isKinematic = true;
    }

    public void Restart()
    {
        GetComponent<Rigidbody>().isKinematic = false;
    }
}
