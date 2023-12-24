using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathChecker : MonoBehaviour {

    public bool isDead;

    void OnTriggerEnter2D(Collider2D collision)
    {
        //if the player collides with the death checker, the enemy is dead
        if (collision.gameObject.tag == "Player")
        {
            isDead = true;
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        //if the player collides with the death checker, the enemy is dead
        if (collision.gameObject.tag == "Player")
        {
            isDead = true;
        }
    }

}
