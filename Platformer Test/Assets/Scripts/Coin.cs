using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

    CoinHandler coinHandler;

    private void Start()
    {
       coinHandler = GameObject.Find("CoinHandler").GetComponent<CoinHandler>();
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        coinHandler.AddToCoins();
        Destroy(gameObject);
    }
}
