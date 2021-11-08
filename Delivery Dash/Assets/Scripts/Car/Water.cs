using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.transform.parent.GetComponentInParent<Rigidbody>().isKinematic = true;
            GameManager.Instance.SceneHandler.GameOver();
        }
    }
}
