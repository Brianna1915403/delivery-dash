using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneHandler : MonoBehaviour
{
    [Header("Pause Menu")]
    [SerializeField] private GameObject m_PauseMenu;

    [Header("Loading Screen")]
    [SerializeField] private GameObject m_LoadingScreen;
    [SerializeField] private Slider m_ProgressBar;
    [Space]
    [SerializeField] private bool m_Randomize = false;

    public bool Randomize
    {
        get { return Randomize; }
        set { m_Randomize = value; }
    }

    public Scene ActiveScene
    {
        get { return SceneManager.GetActiveScene(); }
    }

    public void LoadNextScene() 
    {
        StartCoroutine(LoadNextLevel());
    }    

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && ActiveScene.buildIndex != 0)
        {
            if (m_PauseMenu.activeSelf)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        if (ActiveScene.buildIndex == 0)
        {
            GameObject manager = GameObject.FindGameObjectWithTag("GameController");
            Destroy(manager);
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

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Exit...");
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
    }

    IEnumerator LoadNextLevel()
    {
        m_LoadingScreen.SetActive(true);
        Time.timeScale = 1f;
        m_Randomize = SceneManager.GetActiveScene().buildIndex + 1 > SceneManager.sceneCountInBuildSettings || m_Randomize;
        int index = GetNextSceneIndex();
        AsyncOperation operation = SceneManager.LoadSceneAsync(index);
        while (!operation.isDone)
        {
            float progess = Mathf.Clamp01(operation.progress / 0.9f);
            m_ProgressBar.value = progess;
            yield return null;
        }        
    }

    private int GetNextSceneIndex()
    {
        int index;
        if (m_Randomize)
        {
            index = Random.Range(SceneManager.GetActiveScene().buildIndex - 1, SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            index = SceneManager.GetActiveScene().buildIndex + 1;
        }
        return index;
    }
}
