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
    [SerializeField] private OrderHandeler m_OrderHandeler;

    public ScoreHandler ScoreHandler
    {
        get { return m_ScoreHandler; }
    }

    public OrderHandeler OrderHandeler
    {
        get { return m_OrderHandeler; }
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
