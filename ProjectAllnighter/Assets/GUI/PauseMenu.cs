using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public UnityEvent pause;
    public GameObject player;
    private PlayerController playerController;

    public void Start()
    {
        playerController = player.GetComponent<PlayerController>();        
    }

    public void OnEnable()
    {
        pause.Invoke();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !playerController.inMenu)
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
