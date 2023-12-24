using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackButton : MonoBehaviour {

    public Button Back;
    public GameObject pauseMenu;
    public GameObject controlsMenu;

    // Use this for initialization
    void Start()
    {
        Button backButton = Back.GetComponent<Button>();
        backButton.onClick.AddListener(TaskOnClick);
        pauseMenu = GameObject.Find("PauseMenu");
        controlsMenu = GameObject.Find("ControlsMenu");
    }

    // Update is called once per frame
    void TaskOnClick()
    {
        //SceneManager.LoadScene("OptionsMenu");
        pauseMenu.GetComponent<PauseMenu>().pauseMenuUI.SetActive(true);
        controlsMenu.GetComponent<PauseMenu>().pauseMenuUI.SetActive(false);
    }
}
