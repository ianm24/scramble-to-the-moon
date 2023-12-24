using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionsButton : MonoBehaviour {

    public Button Options;
    public GameObject pauseMenu;
    public GameObject canvas;

	// Use this for initialization
	void Start () {
        Button optionsButton = Options.GetComponent<Button>();
        optionsButton.onClick.AddListener(TaskOnClick);
        pauseMenu = GameObject.Find("PauseMenu");
        canvas = GameObject.Find("Canvas");
    }

    void TaskOnClick()
    {
        //SceneManager.LoadScene("OptionsMenu");
        pauseMenu.GetComponent<PauseMenu>().pauseMenuUI.SetActive(true);
        canvas.SetActive(false);
    }
}
