﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour {

    
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag != "Pit")
        {
            Destroy(gameObject);
        }               
    }
}
