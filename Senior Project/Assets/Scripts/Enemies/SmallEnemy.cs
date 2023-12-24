using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallEnemy : MonoBehaviour {

    public DeathChecker deathChecker;
    public WallChecker wallCheckerL;
    public WallChecker wallCheckerR;
    public float deathTimer;
    public float speed = 2.0f;
    public Vector2 vel;
    Animator anim;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        Rigidbody2D rigid = GetComponent<Rigidbody2D>();

        //checks which direction the enemy is moving
        if (wallCheckerL.direction == 1)
        {
            vel = new Vector2(-speed, rigid.velocity.y);
        }

        if(wallCheckerL.direction == 2)
        {
            vel = new Vector2(speed, rigid.velocity.y);
        }

        deathTimer = .1f;
    }
	
	// Update is called once per frame
	void Update () {
        Rigidbody2D rigid = GetComponent<Rigidbody2D>();

        //if the isDead variable is true, kill the enemy
        if (deathChecker.isDead == true)
        {                   
           deathTimer -= Time.deltaTime;
        }
        if (deathTimer <= 0)
        {
            Destroy(this.gameObject);
        }

        //checks the direction of the enemy
        if (wallCheckerL.direction == 1 || wallCheckerR.direction == 1)
        {
            vel = new Vector2(-speed, rigid.velocity.y);
            anim.SetFloat("speed", speed);
        }

        if (wallCheckerL.direction == 2 || wallCheckerR.direction == 2)
        {
            vel = new Vector2(speed, rigid.velocity.y);
            anim.SetFloat("speed", -speed);
        }
    }
    void FixedUpdate()
    {
        Rigidbody2D rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = vel;
    }
}
