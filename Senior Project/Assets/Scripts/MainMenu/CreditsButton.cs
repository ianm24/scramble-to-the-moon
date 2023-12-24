using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class CreditsButton : MonoBehaviour {

    public Button creditsButton;

	// Use this for initialization
	void Start () {
        creditsButton = this.gameObject.GetComponent<Button>();
        creditsButton.onClick.AddListener(TaskOnClick);
    }
	
	void TaskOnClick()
    {
        SceneManager.LoadScene("Credits");
    }
}
