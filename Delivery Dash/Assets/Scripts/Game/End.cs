using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject gamemanager = GameObject.FindGameObjectWithTag("GameController");
        Destroy(gamemanager);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Exit...");
    }
}
