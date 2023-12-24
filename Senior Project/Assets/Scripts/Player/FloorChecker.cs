using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorChecker : MonoBehaviour {

    public bool isFloored = false;

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Platform" || coll.gameObject.name == "Floor")
        {
            isFloored = true;
        }
             
    }
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Platform" || coll.gameObject.name == "Floor")
        {
            isFloored = true;
        }

    }
    void OnTriggerExit2D(Collider2D coll)
    {
        isFloored = false;
    }
    void OnCollisionExit2D(Collision2D coll)
    {
        isFloored = false;
    }
}
