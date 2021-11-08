using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SceneHandler : MonoBehaviour
{   
    [Header("Story Screen")]
    [SerializeField] private GameObject m_StoryScreen;

    [Header("Pause Menu")]
    [SerializeField] private GameObject m_PauseMenu;

    [Header("Loading Screen")]
    [SerializeField] private GameObject m_LoadingScreen;
    [SerializeField] private Slider m_ProgressBar;

    [Header("Game UI")]
    [SerializeField] private GameObject m_GameUI;
    [SerializeField] private TextMeshProUGUI m_Clock;
    [SerializeField] private TextMeshProUGUI m_CancelationTimer;
    [SerializeField] private Slider m_DamageIndicator;
    [SerializeField] private TextMeshProUGUI m_MoneyCounter;

    [Header("Next Day")]
    [SerializeField] private GameObject m_NextDayUI;
    [SerializeField] private Image m_AverageRating;
    [SerializeField] private TextMeshProUGUI m_CustomersServed;
    [SerializeField] private TextMeshProUGUI m_Debt;
    [SerializeField] private TextMeshProUGUI m_Earnings;
    [SerializeField] private TextMeshProUGUI m_DebtBalance;
    [SerializeField] private TextMeshProUGUI m_Day;

    [Header("Game Over")]
    [SerializeField] private GameObject m_GameOverUI;
    [SerializeField] private Image m_FinalAverageRating;
    [SerializeField] private TextMeshProUGUI m_FinalCustomersServed;
    [SerializeField] private TextMeshProUGUI m_FinalDebt;
    [SerializeField] private TextMeshProUGUI m_FinalDay;

    public string Clock
    {
        set { m_Clock.text = value; }
    }

    public string CancelationTimer
    {
        set { m_CancelationTimer.text = value; }
    }

    public float DamageIndicator
    {
        set { m_DamageIndicator.value = value; }
    }

    public string Money
    {
        set { m_MoneyCounter.text = value; }
    }

    public Scene ActiveScene
    {
        get { return SceneManager.GetActiveScene(); }
    }        

    private void Update()
    {
        
        if (m_DamageIndicator.value >= 1)
        {
            GameOver();
        }
        CheckInput();
    }

    private void CheckInput() {
        if (Input.GetKeyDown(KeyCode.Escape) && ActiveScene.buildIndex != 0)
        {
            if (!m_PauseMenu.activeSelf && Time.timeScale == 0f)
                return;
            if (m_PauseMenu.activeSelf)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }        
    }    

    public void Play()
    {
        if (GameManager.Instance.DataHandler.HasSeenIntro)
        {
            LoadNextScene();
        }
        else
        {
            m_StoryScreen.SetActive(true);
        }
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        m_PauseMenu.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        m_PauseMenu.SetActive(false);
    }

    public void MainMenu()
    {
        StartCoroutine(LoadLevel(0));
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        m_GameOverUI.SetActive(true);
        m_FinalAverageRating.sprite = GameManager.Instance.ScoreHandler.AverageRating;
        m_FinalCustomersServed.text = GameManager.Instance.ScoreHandler.AmountOfCustomers.ToString();
        m_FinalDebt.text = GameManager.Instance.ScoreHandler.Balance.ToString("f2");
        m_FinalDay.text = GameManager.Instance.ScoreHandler.Day.ToString();
    }

    public void NextDay()
    {
        if (m_NextDayUI.activeSelf)
            return;
        Time.timeScale = 0f;
        m_NextDayUI.SetActive(true);
        m_AverageRating.sprite = GameManager.Instance.ScoreHandler.AverageRating;
        m_CustomersServed.text = GameManager.Instance.ScoreHandler.AmountOfCustomers.ToString();
        m_Debt.text = GameManager.Instance.ScoreHandler.Debt.ToString("f2");
        m_Earnings.text = GameManager.Instance.ScoreHandler.Earnings.ToString("f2");
        m_DebtBalance.text = GameManager.Instance.ScoreHandler.Balance.ToString("f2");
        m_Day.text = GameManager.Instance.ScoreHandler.Day.ToString();
        GameManager.Instance.ScoreHandler.Day++;        
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Exit...");
    }

    public void DeactivateLoadingScreen()
    {
        if (m_ProgressBar.value == 1)
        {
            GameManager.Instance.TimeHandler.Moon.enabled = true;
            m_GameUI.SetActive(true);
            GameManager.Instance.IsInGame = true;
            m_LoadingScreen.SetActive(false);
        }
    }

    public void LoadNextScene() 
    {
        m_NextDayUI.SetActive(false);
        StartCoroutine(LoadNextLevel());
    }

    IEnumerator LoadLevel(int index)
    {
        m_LoadingScreen.SetActive(true);
        Time.timeScale = 1f;
        AsyncOperation operation = SceneManager.LoadSceneAsync(index);
        while (!operation.isDone)
        {
            float progess = Mathf.Clamp01(operation.progress / 0.9f);
            m_ProgressBar.value = progess;
            yield return null;
        }
        GameManager.Instance.IsInGame = index != 0;
    }

    IEnumerator LoadNextLevel()
    {
        m_LoadingScreen.SetActive(true);
        GameManager.Instance.OrderHandeler.ClearWaypoints();
        Time.timeScale = 1f;
        int index = SceneManager.GetActiveScene().buildIndex + 1;
        AsyncOperation operation = SceneManager.LoadSceneAsync(index);
        while (!operation.isDone)
        {
            float progess = Mathf.Clamp01(operation.progress / 0.9f);
            m_ProgressBar.value = progess;
            yield return null;
        }        
    }
}
