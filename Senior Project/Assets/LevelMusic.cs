using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMusic : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if (PlayerPrefs.HasKey("musicMuted"))
        {
            if (PlayerPrefs.GetInt("musicMuted") == 1)
            {
                this.GetComponent<AudioSource>().mute = true;
            }
            else
            {
                this.GetComponent<AudioSource>().mute = false;
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
       
	}
}
