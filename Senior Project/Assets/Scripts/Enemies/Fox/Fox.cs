using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fox : MonoBehaviour {

    public DeathChecker deathChecker;
    public WallChecker wallCheckerL;
    public WallChecker wallCheckerR;
    public FloorChecker floorChecker;
    public GameObject bossWall;
    public GameObject bossWall1;
    public PlayerController player;
    public Vector2 jumpForce;
    public Vector2 vel;
    Animator anim;
    public int health;
    public int numberOfJumps;
    public float invincTime;
    public float jumpDelay;
    public float speed = 2.0f;
    public float jumpHeight = 250f;

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

        invincTime = 0;
        health = 2;
        numberOfJumps = 0;        
    }
	
	// Update is called once per frame
	void Update () {
        Rigidbody2D rigid = GetComponent<Rigidbody2D>();

        if (invincTime > 0)
        {
            invincTime -= Time.deltaTime;
        }
        if (invincTime < 0)
        {
            anim.SetBool("Invinc", false);
            invincTime = 0;
            return;
        }

        //if the isDead variable is true, deal damage to the enemy
        if (deathChecker.isDead == true && invincTime <= 0)
        {
            if (health > 0)
            {
                health--;
                invincTime = 2.0f;
                deathChecker.isDead = false;
                anim.SetBool("Invinc", true);
            }
            else
            {
                //bossWall = GameObject.Find("bossWall1Spawn");
                //Destroy(bossWall.gameObject);
                bossWall1 = GameObject.Find("bossWall1Spawn");
                Destroy(bossWall1.gameObject);
                Destroy(this.gameObject);
            }
        }
        //if(speed > 2)
        //{
        //    bossWall1 = GameObject.FindGameObjectWithTag("BossWall1");
        //}
        //if (GameObject.FindGameObjectWithTag("BossWall") == null && GameObject.FindGameObjectWithTag("BossWall1") == null)
        //{
        //    Debug.Log("should be dead");
        //    bossWall = GameObject.FindGameObjectWithTag("BossWall");
        //    Destroy(bossWall.gameObject);
        //    bossWall1 = GameObject.FindGameObjectWithTag("BossWall1");
        //    Destroy(bossWall1.gameObject);
        //    Destroy(this.gameObject);
        //}

        //randomized movement
        //speed up when closer to player
        if (gameObject.transform.position.x <= player.transform.position.x + 6 && gameObject.transform.position.x >= player.transform.position.x - 6 && speed == 2)
        {
            speed = 2f * speed;
        }
        if (gameObject.transform.position.x >= player.transform.position.x + 6 || gameObject.transform.position.x <= player.transform.position.x - 6)
        {
            speed = 2;
        }
        //pounce
        jumpForce = new Vector2(0, 0);
        if (gameObject.transform.position.x <= player.transform.position.x + 5 && gameObject.transform.position.x >= player.transform.position.x - 5 && numberOfJumps < 1)
        {
            jumpForce = new Vector2(0, jumpHeight);
            numberOfJumps++;
            jumpDelay = 5f;
        }
        if (floorChecker.isFloored && jumpDelay <= 0)
        {
            numberOfJumps = 0;
        }
        if (jumpDelay > 0)
        {
            jumpDelay -= Time.deltaTime;
        }
        if (jumpDelay < 0)
        {
            jumpDelay = 0;
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
        rigid.AddForce(jumpForce);
    }
}
