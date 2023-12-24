using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlsButton : MonoBehaviour {

    public Button Controls;
    public GameObject controlsMenu;
    public GameObject pauseMenu;
    public GameObject canvas;

    // Use this for initialization
    void Start()
    {
        Button controlsButton = Controls.GetComponent<Button>();
        controlsButton.onClick.AddListener(TaskOnClick);
        controlsMenu = GameObject.Find("ControlsMenu");
        pauseMenu = GameObject.Find("PauseMenu");
        canvas = GameObject.Find("Canvas");
    }

    void TaskOnClick()
    {
        //SceneManager.LoadScene("OptionsMenu");
        controlsMenu.GetComponent<PauseMenu>().pauseMenuUI.SetActive(true);
        pauseMenu.GetComponent<PauseMenu>().pauseMenuUI.SetActive(false);
        canvas.SetActive(false);
    }
}
