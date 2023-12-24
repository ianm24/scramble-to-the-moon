using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : MonoBehaviour {

        // Update is called once per frame
    void OnTriggerEnter2D(Collider2D coll)
    {
        Destroy(gameObject);
        //seedHandler.AddToSeeds();        
    }
}
