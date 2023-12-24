using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour {

    public GameObject GEgg;

    // Use this for initialization
    void Start () {
        GEgg = GameObject.Find("EggHandler");
	}
	
	// Update is called once per frame
	void Update () {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (SceneManager.GetActiveScene().name == "Level1")
        {
            if (GEgg.GetComponent<EggHandler>().goldenEggExists)
            {
                Debug.Log("Congratulations on getting the eggs");
            }
            SceneManager.LoadScene("Story2");
        }
        if (SceneManager.GetActiveScene().name == "Level2")
        {
            if (GEgg.GetComponent<EggHandler>().goldenEggExists)
            {
                Debug.Log("Congratulations on getting the eggs");
            }
            SceneManager.LoadScene("Story3");
        }
        if (SceneManager.GetActiveScene().name == "Level3")
        {
            if (GEgg.GetComponent<EggHandler>().goldenEggExists)
            {
                Debug.Log("Congratulations on getting the eggs");
            }
            SceneManager.LoadScene("Story4");
        }
    }
}
