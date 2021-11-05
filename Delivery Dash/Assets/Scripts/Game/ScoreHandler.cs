using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreHandler : MonoBehaviour
{
    [Header("Customer Satisfaction")]
    [SerializeField] private GameObject m_CustomerSatisfaction;
    private Slider m_CustomerSatisfactionSlider;
    [SerializeField] private float m_Rating = 5f;
    [Space]
    [SerializeField] private Texture2D m_Devestated;
    [SerializeField] private Texture2D m_Disapointed;
    [SerializeField] private Texture2D m_Okay;
    [SerializeField] private Texture2D m_Satisfied;
    [SerializeField] private Texture2D m_Overjoyed;
    private List<float> m_CustomerRatings = new List<float>();

    [Header("Score Keeping")]
    [SerializeField] private float m_AverageRating = 0f;
    [SerializeField] private Texture2D m_AverageRatingTexture;
    [SerializeField] private int m_AmountOfCustomers = 0;

    [Header("Penalties")]
    [SerializeField] private float m_FirstImpressionPenalty = 1f;
    [SerializeField] private float m_CrashPenalty = 0.5f;
    [SerializeField] private float m_SurfacePenalty = 0.01f;

    public float Rating
    {
        get { return m_Rating; }
        set { m_Rating = value; }
    }

    public Texture2D AverageRating
    {
        get { return m_AverageRatingTexture; }
    }

    public float FirstImpressionPenalty
    {
        get { return m_FirstImpressionPenalty; }
    }

    public float CrashPenalty
    {
        get { return m_CrashPenalty; }
    }

    public float SurfacePenalty
    {
        get { return m_SurfacePenalty; }
    }

    void Awake()
    {
        m_CustomerSatisfactionSlider = m_CustomerSatisfaction.GetComponent<Slider>();
    }

    void Update()
    {
        m_CustomerSatisfactionSlider.value = m_Rating;
    }

    /// <summary>
    /// Sets up for the next client.
    /// </summary>
    public void NextClient()
    {
        m_AmountOfCustomers++;
        AddRating();
    }

    /// <summary>
    /// Adds a rating to the list, updates the average rating.
    /// </summary>
    private void AddRating()
    {
        m_CustomerRatings.Add(m_Rating);
        UpdateAverageRating();
        ResetRating();
    }

    /// <summary>
    /// Computes the average rating across all customers, and updates the texture.
    /// </summary>
    private void UpdateAverageRating()
    {
        float sum = 0f;
        foreach (float rating in m_CustomerRatings)
        {
            sum += rating;
        }

        m_AverageRating = sum / m_CustomerRatings.Count;
        UpdateAverageRatingTexture();
    }

    /// <summary>
    /// Sets the texture depending on the average rating.
    /// </summary>
    private void UpdateAverageRatingTexture()
    {
        if (m_AverageRating <= 5 && m_AverageRating > 4)
        {
            m_AverageRatingTexture = m_Overjoyed;
        }
        else if (m_AverageRating <= 4 && m_AverageRating > 3)
        {
            m_AverageRatingTexture = m_Satisfied;
        }
        else if (m_AverageRating <= 3 && m_AverageRating > 2)
        {
            m_AverageRatingTexture = m_Okay;
        }
        else if (m_AverageRating <= 2 && m_AverageRating > 1)
        {
            m_AverageRatingTexture = m_Disapointed;
        }
        else if (m_AverageRating <= 1)
        {
            m_AverageRatingTexture = m_Devestated;
        }
    }    

    /// <summary>
    /// Resets the rating to 5.
    /// </summary>
    private void ResetRating()
    {
        m_Rating = 5;
    }
}
