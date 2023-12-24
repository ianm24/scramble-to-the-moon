using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorChecker : MonoBehaviour {

    public bool isFloored = false;

    void OnTriggerEnter2D(Collider2D coll)
    {
        isFloored = true;
    }
    void OnTriggerExit2D(Collider2D coll)
    {
        isFloored = false;
    }
}
