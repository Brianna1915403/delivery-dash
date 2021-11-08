using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [Header("Scene")]
    [SerializeField] private SceneHandler m_SceneHandler;
    [SerializeField] private bool m_IsInGame = false;

    [Header("Score Keeping")]
    [SerializeField] private ScoreHandler m_ScoreHandler;

    [Header("Customer Spawning")]
    [SerializeField] private OrderHandeler m_OrderHandeler;
    [SerializeField] private bool m_IsOccupied = false;
    [SerializeField] private float m_MinOrderTime = 1f;
    [SerializeField] private float m_MaxOrderTime = 5f;

    [Header("Time")]
    [SerializeField] private TimeHandler m_TimeHandler;

    [Header("Save Data")]
    [SerializeField] private DataHandler m_DataHandler;


    public SceneHandler SceneHandler
    {
        get { return m_SceneHandler; }
    }

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

    public DataHandler DataHandler
    {
        get { return m_DataHandler; }
    }

    public bool IsInGame
    {
        get { return m_IsInGame; }
        set { m_IsInGame = value; }
    }

    public bool IsOccupied
    {
        get { return m_IsOccupied; }
        set { m_IsOccupied = value; }
    }
    

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void Start()
    {        
            StartGame();
    }

    public void StartGame()
    {
        StartCoroutine(SpawnCustomers());
    }

    IEnumerator SpawnCustomers()
    {        
        while (true)
        {
            if (m_IsInGame)
            {
                float ordertime = Random.Range(m_MinOrderTime, m_MaxOrderTime);
                yield return new WaitForSeconds(ordertime);
                if (!m_IsOccupied)
                {
                    m_IsOccupied = OrderHandeler.SpawnCustomer();
                }
            } 
            else
            {
                yield return new WaitForSecondsRealtime(1f);
            }
        }
        
    }
}
