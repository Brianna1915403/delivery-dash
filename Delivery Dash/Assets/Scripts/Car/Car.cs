using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField] private float m_Health = 100f;
    [SerializeField] private float m_FrontBumperDamage = 0f;
    [SerializeField] private float m_BackBumperDamage = 0f;

    public float Health {
        set { m_Health = value; }
        get { return m_Health; }
    }

    public float BackBumperDamage {
        set { m_BackBumperDamage = value; }
        get { return m_BackBumperDamage; }
    }

    public float FrontBumperDamage {
        set { m_FrontBumperDamage = value; }
        get { return m_FrontBumperDamage; }
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
