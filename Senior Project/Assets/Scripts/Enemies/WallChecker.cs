using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallChecker : MonoBehaviour {

    public int direction;
    public bool wallCheck;
    public WallChecker wallCheckerL;
    public WallChecker wallCheckerR;

	// Use this for initialization
	void Start () {
        direction = 1;
	}
	
	// Update is called once per frame
	void Update () {

        //switches direction when wall check is true
		if (wallCheck && direction == 1)
        {
            wallCheckerR.direction = 2;
            wallCheck = false;
        }
        if (wallCheck && direction == 2)
        {
            direction = 1;
            wallCheckerL.direction = 1;
            wallCheck = false;
        }

	}

    void OnTriggerEnter2D(Collider2D collision)
    {
        //if the enemy collides with the platform, the wallcheck is true
        if (collision.gameObject.tag == "Platform" || collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "BossWall1" || collision.gameObject.tag == "ExtraWall" || collision.gameObject.tag == "ExtraWall2" || collision.gameObject.name == "bossWall1Spawn")
        {
            wallCheck = true;
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.gameObject.tag == "Platform" || collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "BossWall1" || collision.gameObject.tag == "ExtraWall" || collision.gameObject.tag == "ExtraWall2" || collision.gameObject.name == "bossWall1Spawn")
        {
            wallCheck = true;
        } 
    }
}
