using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EggHandler : MonoBehaviour {

    public int egg1;
    public int egg2;
    public int egg3;
    public Text eggsNumber1;
    public Text eggsNumber2;
    public Text eggsNumber3;
    public bool goldenEggExists;
    public Transform goldenEgg;

    // Use this for initialization
    void Start () {
        goldenEggExists = false;
    }
	
	// Update is called once per frame
	void Update () {
        //tracks which eggs the player has collected
        egg1 = GameObject.FindGameObjectsWithTag("Egg1").Length;
        egg2 = GameObject.FindGameObjectsWithTag("Egg2").Length;
        egg3 = GameObject.FindGameObjectsWithTag("Egg3").Length;

        if (egg1 < 1)
        {
            eggsNumber1.text = "y";
        }
        if (egg2 < 1)
        {
            eggsNumber2.text = "y";
        }
        if (egg3 < 1)
        {
            eggsNumber3.text = "y";
        }
        
        //spawns golden egg if all other eggs are collected
        if (egg1 == 0 && egg2 == 0 && egg3 == 0 && goldenEggExists == false)
        {
            if(SceneManager.GetActiveScene().name == "Level1")
            {
                goldenEggExists = true;
                Instantiate(goldenEgg, new Vector2(169.37f, -13.62f), goldenEgg.rotation);
                PlayerPrefs.SetInt("lvl1Egg", 1);
            }
            if (SceneManager.GetActiveScene().name == "Level2")
            {
                goldenEggExists = true;
                Instantiate(goldenEgg, new Vector2(492.27f, -23.68f), goldenEgg.rotation);
                PlayerPrefs.SetInt("lvl2Egg", 1);
            }
            if (SceneManager.GetActiveScene().name == "Level3")
            {
                goldenEggExists = true;
                Instantiate(goldenEgg, new Vector2(87.42f, 65.82f), goldenEgg.rotation);
                PlayerPrefs.SetInt("lvl3Egg", 1);
            }
        }
    }
}
