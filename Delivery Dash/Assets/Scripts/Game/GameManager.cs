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

    [Header("Score Keeping")]
    [SerializeField] private float m_AverageRating = 0f;
    [SerializeField] private int m_AmountOfCustomers = 0;
    [SerializeField] private List<float> m_CustomerRatings = new List<float>();
    [Space]
    [SerializeField] private float m_Rating;

    [Header("Customer Spawning")]
    [SerializeField] private OrderHandeler m_OrderHandeler;

    public float Rating {
        get { return m_Rating; }
        set { m_Rating = value; }
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

    // --- SCORE KEEPING START ---

    public void AddRating()
    {
        m_CustomerRatings.Add(m_Rating);
        UpdateAverageRating();
        ResetRating();
    }

    public void UpdateAverageRating() 
    {
        float sum = 0f;
        foreach (float rating in m_CustomerRatings)
        {
            sum += rating;
        }

        m_AverageRating = sum / m_CustomerRatings.Count;
    }

    public void NextClient() 
    {
        m_AmountOfCustomers++;
        AddRating();
        ResetRating();
    }

    private void ResetRating() {
        m_Rating = 5;
    }

    // --- SCORE KEEPING END ---
}
