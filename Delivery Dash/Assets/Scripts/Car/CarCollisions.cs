using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCollisions : MonoBehaviour
{
    [SerializeField] private Vector3 m_Size;
    [SerializeField] private float m_YOffset;
    [SerializeField] private float m_ZOffset;

    [SerializeField] private CarController m_CarController;
    [SerializeField] private Car m_Car;
    [SerializeField] private bool m_IsFrontBumper;

    // Start is called before the first frame update
    void Start()
    {
        Collider[] colliders = Physics.OverlapBox(transform.position, Vector3.one);
        foreach (Collider collider in colliders) {
            if (collider.gameObject.CompareTag("Building"))
                Debug.Log("Damage Taken!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Respawn() {
        m_CarController.Brake(true);
        transform.rotation = new Quaternion(0, 0, 0, 1);
        transform.position = new Vector3(30, 2, 61);
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(new Vector3(transform.position.x, transform.position.y + m_YOffset, transform.position.z + m_ZOffset), m_Size);
    }
}
