using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SFXToggle : MonoBehaviour {

    public AudioSource ButtonClickAudio;
    public Toggle sfxToggle;
  
    void Start()
    {
        if(GameObject.Find("ButtonClickAudio") != null)
        {
            ButtonClickAudio = GameObject.Find("ButtonClickAudio").GetComponent<AudioSource>();
        }
        

        if (ButtonClickAudio != null)
        {
            if (PlayerPrefs.HasKey("sfxMuted"))
            {
                if (PlayerPrefs.GetInt("sfxMuted") == 1)
                {
                    sfxToggle.isOn = false;
                    ButtonClickAudio.mute = true;
                }
                else
                {
                    sfxToggle.isOn = true;
                    ButtonClickAudio.mute = false;
                }
            }
        }               
    }
    public void OnChangeValue()
    {
        if (ButtonClickAudio != null)
        {
            if (sfxToggle.isOn)
            {
                ButtonClickAudio.mute = false;
                PlayerPrefs.SetInt("sfxMuted", 0);
            }
            else
            {
                ButtonClickAudio.mute = true;
                PlayerPrefs.SetInt("sfxMuted", 1);
            }
        }        
    }
}

