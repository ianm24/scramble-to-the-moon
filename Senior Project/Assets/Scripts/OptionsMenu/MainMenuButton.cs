using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuButton : MonoBehaviour {

    public GameObject pauseMenu;
    public GameObject canvas;
    public Button MainMenu;
    public AudioSource ButtonClickAudio;

    // Use this for initialization
    void Start () {
        Button mainMenuButton = MainMenu.GetComponent<Button>();
        mainMenuButton.onClick.AddListener(TaskOnClick);
        ButtonClickAudio = GameObject.Find("ButtonClickAudio").GetComponent<AudioSource>();
    }

    void TaskOnClick()
    {
        pauseMenu.GetComponent<PauseMenu>().pauseMenuUI.SetActive(false);
        canvas.SetActive(true);
        ButtonClickAudio.Play();
    }
}
