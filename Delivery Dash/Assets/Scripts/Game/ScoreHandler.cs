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
    [SerializeField] private int m_AmountOfCustomers = 0;

    [Header("Penalties")]
    [SerializeField] private float m_FirstImpressionPenalty = 1f;
    [SerializeField] private float m_CrashPenalty = 0.5f;
    [SerializeField] private float m_SurfacePenalty = 0.1f;

    public float Rating
    {
        get { return m_Rating; }
        set { m_Rating = value; }
    }

    public Texture2D AverageRating
    {
        get { return m_Okay; }
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        m_CustomerSatisfactionSlider.value = m_Rating;
    }

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

    private void ResetRating()
    {
        m_Rating = 5;
    }
}
