using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicToggle : MonoBehaviour {

    public Toggle musicToggle;
    public AudioSource MusicAudio;

    void Start()
    {
        MusicAudio = GameObject.Find("LevelMusic").GetComponent<AudioSource>();

        if (PlayerPrefs.HasKey("musicMuted"))
        {
            if (PlayerPrefs.GetInt("musicMuted") == 1)
            {
                musicToggle.isOn = false;
                MusicAudio.mute = true;
            }
            else
            {
                musicToggle.isOn = true;
                MusicAudio.mute = false;
            }
        }
    }

    public void OnChangeValue()
    {
        if (musicToggle.isOn)
        {
            MusicAudio.mute = false;
            PlayerPrefs.SetInt("musicMuted", 0);
        }
        else
        {
            MusicAudio.mute = true;
            PlayerPrefs.SetInt("musicMuted", 1);
        }
    }
}
