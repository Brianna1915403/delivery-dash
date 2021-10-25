using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCollisions : MonoBehaviour
{
    [SerializeField] private CarController m_CarController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Water")) {
            Respawn();
        }
    }

    private void Respawn() {
        m_CarController.Brake(true);
        transform.rotation = new Quaternion(0, 0, 0, 1);
        transform.position = new Vector3(30, 2, 61);
    }
}
