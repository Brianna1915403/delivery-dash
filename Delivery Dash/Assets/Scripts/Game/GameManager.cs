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

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // --- SCORE KEEPING START ---

    public void InsertRating(float rating)
    {
        m_CustomerRatings.Add(rating);
        UpdateAverageRating();
    }

    public void UpdateAverageRating() {
        float sum = 0f;
        foreach (float rating in m_CustomerRatings)
        {
            sum += rating;
        }

        m_AverageRating = sum / m_CustomerRatings.Count;

    }

    // --- SCORE KEEPING END ---
}
