﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager: MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("q"))
        {
            Application.Quit();
        }

    }
    public void StartGame()
    {
        SceneManager.LoadScene(3);
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}
