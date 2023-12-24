using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour {

    public AudioSource ButtonClickAudio;
    public AudioSource MusicAudio;

    // Use this for initialization
    void Start () {
        MusicAudio = GameObject.Find("LevelMusic").GetComponent<AudioSource>();
        ButtonClickAudio = GameObject.Find("ButtonClickAudio").GetComponent<AudioSource>();

        if (PlayerPrefs.HasKey("musicMuted"))
        {
            if (PlayerPrefs.GetInt("musicMuted") == 1)
            {
                MusicAudio.mute = true;
            }
            else
            {
                MusicAudio.mute = false;
            }
        }
        if (PlayerPrefs.HasKey("sfxMuted"))
        {
            if (PlayerPrefs.GetInt("sfxMuted") == 1)
            {
                ButtonClickAudio.mute = true;
            }
            else
            {
                ButtonClickAudio.mute = false;
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
