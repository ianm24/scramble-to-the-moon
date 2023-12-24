using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearCompletionButton : MonoBehaviour {

    public Button clear;
    public GameObject ComPletion;

    // Use this for initialization
    void Start()
    {
        Button clearButton = clear.GetComponent<Button>();
        clearButton.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        PlayerPrefs.SetInt("lvl1Egg", 0);
        PlayerPrefs.SetInt("lvl2Egg", 0);
        PlayerPrefs.SetInt("lvl3Egg", 0);
        ComPletion.SetActive(false);
    }
}
