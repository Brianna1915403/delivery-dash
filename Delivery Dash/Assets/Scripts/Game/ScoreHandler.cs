using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreHandler : MonoBehaviour
{
    [Header("Customer Satisfaction")]
    [SerializeField] private Slider m_CustomerSatisfaction;
    [SerializeField] private float m_Rating = 5f;
    [Space]
    [SerializeField] private Sprite m_Devestated;
    [SerializeField] private Sprite m_Disapointed;
    [SerializeField] private Sprite m_Okay;
    [SerializeField] private Sprite m_Satisfied;
    [SerializeField] private Sprite m_Overjoyed;
    private List<float> m_CustomerRatings = new List<float>();

    [Header("Score Keeping")]
    [SerializeField] private float m_AverageRating = 0f;
    [SerializeField] private Sprite m_AverageRatingTexture;
    [SerializeField] private int m_AmountOfCustomers = 0;
    [Space]
    [SerializeField] private float m_Debt = 20000f;
    [SerializeField] private float m_Balance;
    [SerializeField] private float m_Earnings = 0f;
    [SerializeField] private float m_TotalEarnings = 0f;
    [Space]
    [SerializeField] private int m_Day = 1;

    [Header("Penalties")]
    [SerializeField] private float m_FirstImpressionPenalty = 1f;
    [SerializeField] private float m_CrashPenalty = 0.5f;
    [SerializeField] private float m_SurfacePenalty = 0.01f;

    public float Rating
    {
        get { return m_Rating; }
        set { m_Rating = value; }
    }

    public Sprite AverageRating
    {
        get { return m_AverageRatingTexture; }
    }

    public int AmountOfCustomers
    {
        get { return m_AmountOfCustomers; }
    }

    public float Debt
    {
        get { return m_Debt; }
    }

    public float Balance
    {
        get { return m_Balance; }
    }

    public float Earnings
    {
        get { return m_Earnings; }
    }

    public float TotalEarnings
    {
        get { return m_TotalEarnings; }
    }

    public int Day
    {
        get { return m_Day; }
        set { m_Day = value; }
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

    private void Start()
    {
        m_Balance = m_Debt;
    }

    void Update()
    {
        m_CustomerSatisfaction.value = m_Rating;
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
        UpdateEarnings();
        m_CustomerRatings.Add(m_Rating);
        UpdateAverageRating();
        ResetRating();
    }

    private void UpdateEarnings()
    {
        m_Earnings += ConvertRatingToEarnings(m_Rating);
        m_Balance -= m_Earnings;
        GameManager.Instance.SceneHandler.Money = m_Earnings.ToString("f2");
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
        else
        {
            m_AverageRatingTexture = m_Devestated;
        }
    }    

    private float ConvertRatingToEarnings(float rating)
    {
        if (rating <= 5 && rating > 4)
        {
            return Random.Range(40f, 50f);
        }
        else if (rating <= 4 && rating > 3)
        {
            return Random.Range(30f, 40f);
        }
        else if (rating <= 3 && rating > 2)
        {
            return Random.Range(20f, 30f);
        }
        else if (rating <= 2 && rating > 1)
        {
            return Random.Range(10f, 20f);
        }
        return 0f;
    }

    /// <summary>
    /// Resets the rating to 5.
    /// </summary>
    private void ResetRating()
    {
        m_Rating = 5;
    }

    /// <summary>
    /// Resets Earnings and Debt.
    /// </summary>
    private void ResetEarnings()
    {
        m_Debt = m_Balance;
        m_TotalEarnings += m_Earnings;
        m_Earnings = 0f;
    }
}
