using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public bool Paused = false;
    public GameObject pauseMenuUI;
    public GameObject canvas;

    private void Start()
    {
        pauseMenuUI.SetActive(false);
    }
    // Update is called once per frame
    void Update () {
        if(SceneManager.GetActiveScene().name != "MainMenu")
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (Paused)
                {
                    Resume();
                }
                else
                {
                    Pause();
                    if (this.gameObject.name == "ControlsMenu")
                    {
                        pauseMenuUI.SetActive(false);
                    }
                }
            }
        }		
	}


    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
        Paused = false;
        canvas.SetActive(true);
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0;
        Paused = true;
        canvas.SetActive(false);
    }

}
