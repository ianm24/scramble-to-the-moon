using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CoinHandler : MonoBehaviour {

    public int collectedCoins = 0;
    public int maxCoins = 5;
    public Text coinNumber;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        coinNumber.text = collectedCoins + "";
        if(collectedCoins == maxCoins)
        {
            SceneManager.LoadScene("main");
        }
	}

    public void AddToCoins()
    {
        collectedCoins++;
    }
}
