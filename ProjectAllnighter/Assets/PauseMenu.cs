using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenuUI.SetActive(true);
        }
    }

    public void Exit()
    {
        Application.Quit();
        Debug.Log("quit");
    }

}
