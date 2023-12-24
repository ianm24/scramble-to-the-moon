using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalFox : MonoBehaviour
{

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
    public float health;
    public int numberOfJumps;
    public float laserShots;
    public float invincTime;
    public float jumpDelay;
    public float speed = 2.0f;
    public float jumpHeight = 250f;
    public bool phaseTwo = false;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        Rigidbody2D rigid = GetComponent<Rigidbody2D>();

        //checks which direction the enemy is moving
        if (wallCheckerL.direction == 1)
        {
            vel = new Vector2(-speed, rigid.velocity.y);
        }

        if (wallCheckerL.direction == 2)
        {
            vel = new Vector2(speed, rigid.velocity.y);
        }

        invincTime = 0;
        health = 5;
        numberOfJumps = 0;
    }

    // Update is called once per frame
    void Update()
    {
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

        if (health <= 2 && !phaseTwo)
        {
            phaseTwo = true;
            if (GameObject.Find("CrumbleFloor") != null)
            {
                Destroy(GameObject.Find("CrumbleFloor"));
            }
            else
            {
                Debug.Log("hi");
                Destroy(GameObject.Find("CrumbleFloor(Clone)"));
            }
            player.collapse = true;

        }

        //if the isDead variable is true, deal damage to the enemy
        if (deathChecker.isDead == true && invincTime <= 0)
        {
            if (health > 0)
            {
                health = health - 1;
                invincTime = 2.0f;
                deathChecker.isDead = false;
                anim.SetBool("Invinc", true);
            }
            if (health <= 0)
            {
                //bossWall = GameObject.Find("bossWall1Spawn");
                //Destroy(bossWall.gameObject);
                bossWall1 = GameObject.Find("bossWall1Spawn");
                Destroy(bossWall1.gameObject);
                Destroy(this.gameObject);
            }
        }

        if (phaseTwo)
        {
            if (gameObject.transform.position.x <= player.transform.position.x + 6 && gameObject.transform.position.x >= player.transform.position.x - 6 && speed == 2)
            {
                speed = 2f * speed;
            }
            if (gameObject.transform.position.x >= player.transform.position.x + 6 || gameObject.transform.position.x <= player.transform.position.x - 6)
            {
                speed = 4;
            }
        }
        else
        {
            if (gameObject.transform.position.x <= player.transform.position.x + 6 && gameObject.transform.position.x >= player.transform.position.x - 6 && speed == 2)
            {
                speed = 2f * speed;
            }
            if (gameObject.transform.position.x >= player.transform.position.x + 6 || gameObject.transform.position.x <= player.transform.position.x - 6)
            {
                speed = 2;
            }
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
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "LGPProjectile")
        {
            LaserHit();
            return;
        }
    }
    //void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "LGPProjectile")
    //    {
    //        LaserHit();
    //    }
    //}
    void LaserHit()
    {
        if (health > 0)
        {
            if (laserShots >= 4)
            {                
                deathChecker.isDead = false;
                invincTime = 2.0f;
                anim.SetBool("Invinc", true);
                laserShots = 0;
                health = health - .25f;
                return;
            }
            else
            {               
                deathChecker.isDead = false;
                laserShots = laserShots + .5f;
                health = health - .125f;
                return;
            }
            
            
        }
        if (health <= 0)
        {
            bossWall1 = GameObject.Find("bossWall1Spawn");
            Destroy(bossWall1.gameObject);
            Destroy(this.gameObject);
        }
    }

    void FixedUpdate()
    {
        Rigidbody2D rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = vel;
        rigid.AddForce(jumpForce);
    }
}