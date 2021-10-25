using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField] private float m_Health = 100f;
    [SerializeField] private float m_Penalty;

    public float Health {
        set { m_Health = value; }
        get { return m_Health; }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log($"Car Health: {m_Health} | Front Bumper: {m_FrontBumperDamage} | Back Bumper: {m_BackBumperDamage}");
    }
}
