using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SeedHandler : MonoBehaviour {

    //public int collectedSeeds = 0;
    public Text seedNumber;
    public Text livesNumber;
    public int totalLives;
    public int numberOfLives;
    public int numberOfSeeds;
    public int numberOfSeedBags;
    public int totalSeeds;
    public int currentSeeds;
    public int totalSeedBags;
    public int currentSeedBags;

	// Use this for initialization
	void Start () {
        if (PlayerPrefs.HasKey("lives"))
        {
            numberOfLives = PlayerPrefs.GetInt("lives");
            totalLives = PlayerPrefs.GetInt("lives");
        }
        totalSeeds = GameObject.FindGameObjectsWithTag("Seed").Length;
        totalSeedBags = GameObject.FindGameObjectsWithTag("SeedBag").Length;
    }
	
	// Update is called once per frame
	void Update () {
        //makes arrays of how many seeds there are
        currentSeeds = GameObject.FindGameObjectsWithTag("Seed").Length;
        currentSeedBags = GameObject.FindGameObjectsWithTag("SeedBag").Length;

        //when collected seeds reaches 100, add a life
        if (seedNumber.text == "100")
        {
            totalSeeds = GameObject.FindGameObjectsWithTag("Seed").Length;
            numberOfSeeds = 0;
            seedNumber.text = numberOfSeeds.ToString();
            numberOfLives++;
            livesNumber.text = numberOfLives.ToString();
        }

        //when a seed is collected, add one to the count
        if (totalSeeds > currentSeeds)
        {
            numberOfSeeds = totalSeeds - currentSeeds;
            seedNumber.text = numberOfSeeds.ToString();
        }
        if (totalSeedBags > currentSeedBags)
        {
            numberOfSeedBags = totalSeedBags - currentSeedBags;
            numberOfLives = totalLives + numberOfSeedBags;
            livesNumber.text = numberOfLives.ToString();
        }
        livesNumber.text = numberOfLives.ToString();
    }

    //public void addtoseeds()
    //{
    //    collectedseeds++;
    //}
}
