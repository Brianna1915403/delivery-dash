using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RATING { }

public class GameManager : MonoBehaviour
{
    #region Singleton
    private static GameManager m_Instance;
    public static GameManager Instance { 
        get {
            if (!m_Instance)
                m_Instance = FindObjectOfType<GameManager>();            
            return m_Instance; 
        } 
    }
    #endregion

    [Header("Score Keeping")]
    [SerializeField] private ScoreHandler m_ScoreHandler;

    [Header("Customer Spawning")]
    [SerializeField] private GameObject m_OrderHandlerObject;
    [SerializeField] private OrderHandeler m_OrderHandeler;

    [Header("Time")]
    [SerializeField] private TimeHandler m_TimeHandler;
    [SerializeField] private bool m_IsOnShift = true;

    public ScoreHandler ScoreHandler
    {
        get { return m_ScoreHandler; }
    }

    public OrderHandeler OrderHandeler
    {
        get { return m_OrderHandeler; }
    }

    public TimeHandler TimeHandler
    {
        get { return m_TimeHandler; }
    }

    public bool IsOnShift
    {
        get { return m_IsOnShift; }
        set { m_IsOnShift = value; }
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnCustomers", 1f, 5f);
        Debug.Log("Biggus Dickus");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnCustomers()
    {
        Debug.Log("Invoked...");
        if (IsOnShift)
            OrderHandeler.SpawnCustomer();
    }
}
