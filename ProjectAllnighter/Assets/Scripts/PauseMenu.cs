using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public PlayerInput playerInput;
    

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale=0;
            pauseMenuUI.SetActive(true);
        }
    }
    public void Resume()
    {
        Time.timeScale= 1;
        pauseMenuUI.SetActive(false);   
    }

    public void Exit()
    {
        Application.Quit();
        Debug.Log("quit");
    }

}
