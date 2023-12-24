using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public Vector2 vel;
    public Vector2 jumpForce;       
    public FloorChecker floorChecker;
    public SeedHandler seedHandler;
    public GameObject checkpoint;
    public Transform bossWall1;
    public Transform extraWallT;
    public Transform extraWall2T;
    public Transform bossTriggerT;
    public Transform DoubleJumpPowerup;
    public Transform LaserGunPowerup;
    public Transform LGPProjectile;
    public Transform CrumblePlatform;
    public Transform WaitEnemies;
    public Transform WaitPlatforms;
    public GameObject Projectile;
    public GameObject bossWall;
    public GameObject extraWall;
    public GameObject extraWall2;
    public GameObject bossTrigger;
    public GameObject movingPlat;
    public AudioSource MusicAudio;
    public Text healthNumber;
    public Fox fox;
    public FinalFox finalFox;
    Animator anim;
    public float speed = 20f;
    public float jumpHeight = 100f;
    public float glideTime = .34f;
    public float invincTime = 0f;
    public float killJumpTime = 0f;
    public float shootDelay;
    public float platVel;
    public float platVelY;
    public float checkpointDJPx;
    public float checkpointDJPy;
    public float checkpointLGPx;
    public float checkpointLGPy;
    public int numberOfJumps;
    public int numberOfLives;
    public int killJump;
    public bool isDoubleJump;
    public bool canShoot;
    public bool poweredUp;
    public bool enemyCollide;
    public bool exWallsExist;
    public bool activeWall;
    public bool floor;
    public bool direction;
    public bool missing = false;
    public bool NuLl = false;
    public bool none = false;
    public bool collapse;


    // Use this for initialization
    void Start () {
        vel = new Vector2(0, 0);
        isDoubleJump = false;
        anim = GetComponent<Animator>();
        killJump = 0;
        activeWall = false;
        movingPlat = null;
        healthNumber.text = "1";
        if (PlayerPrefs.GetInt("lives") > 6)
        {
            PlayerPrefs.SetInt("lives", 5);
        }
        //MusicAudio = GameObject.Find("MusicAudio").GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
		Rigidbody2D rigid = GetComponent<Rigidbody2D>();
        floor = floorChecker.isFloored;
        Lives();
        //Debug.Log("#" + seedHandler.numberOfLives);
        //Debug.Log("total" + seedHandler.totalLives);

        //Walk
        vel = new Vector2(Input.GetAxis("Horizontal") * speed, rigid.velocity.y);
        anim.SetFloat("speed", rigid.velocity.x);
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            anim.SetBool("DirectionLisF", false);
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            anim.SetBool("DirectionLisF", true);
        }

        if (vel.x > 0)
        {
            direction = true;
        }
        if (vel.x < 0)
        {
            direction = false;
        }

        //Run
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            speed = 7f; 
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
        {
            speed = 4f;
        }

        //Jump
        //Checks if the player is pressing space on the ground to jump
        jumpForce = new Vector2(0, 0);
        if(Input.GetKey(KeyCode.Space) && floorChecker.isFloored)
        {
            jumpForce = new Vector2(0, jumpHeight);
            numberOfJumps++;
        }
        //Allows for variable jump height based on how long space is pressed
        if (Input.GetKeyUp(KeyCode.Space) && glideTime > 0 && numberOfJumps <= 0)
        {
            vel = new Vector2(rigid.velocity.x, 0);
        }

        //reset level
        if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Time.timeScale = 1;
        }

        //health
        if (poweredUp)
        {
            healthNumber.text = "2";
        }

        //Glide
        //Checks if the player is pressing the space bar after a jump and while they have glide time left
            if (Input.GetKey(KeyCode.Space) && floorChecker.isFloored == false && rigid.velocity.y <= 0 && glideTime > 0 && isDoubleJump == false)
        {
            glideTime -= Time.deltaTime;
            jumpForce = new Vector2(0, 45f);
        }
        //Resets Glide Time and jumps
        if (floorChecker.isFloored)
        {
            glideTime = 0.0425f;
            numberOfJumps = 0;
        }

        //Double Jump
        //Checks if double jump is active and allows double jump if they havent already double jumped
        if (Input.GetKey(KeyCode.Space) && rigid.velocity.y <= 0 && glideTime > 0 && isDoubleJump == true && numberOfJumps < 1 && floorChecker.isFloored == false)
        {
            jumpForce = new Vector2(0, jumpHeight);
            vel = new Vector2(rigid.velocity.x, 0);
            numberOfJumps++;
        }

        //laser gun shooting
        if (Input.GetKey(KeyCode.C) && shootDelay <= 0 && canShoot|| Input.GetKey(KeyCode.E) && shootDelay <= 0 && canShoot)
        {
            Instantiate(LGPProjectile, new Vector2(this.transform.position.x, transform.position.y), new Quaternion(0, 0, 0, 0));
            Projectile = LGPProjectile.gameObject;
            shootDelay = 1.1f;            
        }
        if (shootDelay > 0)
        {
            shootDelay -= Time.deltaTime;
        }

        //temporary invincibility
        if (invincTime > 0)
        {
            invincTime -= Time.deltaTime;
        }
        if (invincTime <= 0)
        {
            anim.SetBool("Invincible", false);
        }

        //jump after killing enemym
        if (killJump == 1 && killJumpTime <= 0)
        {
            killJump = 0;
            jumpForce = new Vector2(0, 1.5f *jumpHeight);
        }
        if (killJump == 2 && killJumpTime <= 0)
        {
            killJump = 0;
            jumpForce = new Vector2(0, 1.7f * jumpHeight);
        }
        if (killJumpTime > 0)
        {
            killJumpTime -= Time.deltaTime;
        }

        //prevents rocketing
        if (vel[1] > 5.5)
        {
            vel[1] = 5;
        }
        if (vel[1] < -5.5)
        {
            vel[1] = -5;
        }

        //makes moving platforms work better
        if (platVel != 0)
        {
            vel = new Vector2(platVel + Input.GetAxis("Horizontal") * speed, rigid.velocity.y); 
        }
        if (movingPlat != null && movingPlat.gameObject.GetComponent<Rigidbody2D>().velocity.x != platVel)
        {
            platVel = movingPlat.gameObject.GetComponent<Rigidbody2D>().velocity.x;
        }


        //level2 stuff
        if (GameObject.FindGameObjectsWithTag("WaitEnemy").Length > 1)
        {
            Destroy(GameObject.FindGameObjectWithTag("WaitEnemy"));
        }
        if (GameObject.FindGameObjectsWithTag("WaitPlatform").Length > 1)
        {
            Destroy(GameObject.FindGameObjectWithTag("WaitPlatform"));
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Rigidbody2D rigid = GetComponent<Rigidbody2D>();

        //you die when you fall in a pit
        numberOfLives = seedHandler.numberOfLives;
        if (collision.gameObject.tag == "Pit")
        {
            Death();
        }
        
        if (collision.gameObject.tag == "Enemy" && invincTime <= 0)
        {
            enemyCollide = true;
            Damage();
        }
        if (collision.gameObject.tag == "WaitEnemy" && invincTime <= 0)
        {
            enemyCollide = true;
            Damage();
        }

        //jump when you hit an enemy
        if (collision.gameObject.tag == "DeathChecker")
        {
            killJump = 1;
            killJumpTime = .015625f;
        }
        if (collision.gameObject.name == "FoxDeathChecker")
        {
            killJump = 2;
            killJumpTime = .015625f;
        }

        //when you get the doublejump powerup, isdoublejump is true
        if (collision.gameObject.tag == "DJP")
        {
            isDoubleJump = true;
            poweredUp = true;
        }

        if(collision.gameObject.tag == "LGP")
        {
            poweredUp = true;
            canShoot = true;
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "Checkpoint")
        {
            checkpoint = collision.gameObject;
            checkpointDJPx = checkpoint.GetComponent<Checkpoint>().DJPx;
            checkpointDJPy = checkpoint.GetComponent<Checkpoint>().DJPy;
            checkpointLGPx = checkpoint.GetComponent<Checkpoint>().LGPx;
            checkpointLGPy = checkpoint.GetComponent<Checkpoint>().LGPy;
            //if (checkpoint.GetComponent<Checkpoint>().LGP != null)
            //{
            //    checkpointLGPx = checkpoint.GetComponent<Checkpoint>().LGPx;
            //    checkpointLGPy = checkpoint.GetComponent<Checkpoint>().LGPy;
            //}
        }

        //activate boss
        if (collision.gameObject.tag == "BossTrigger" && activeWall == false)
        {
            if (SceneManager.GetActiveScene().name == "Level1")
            {
                activeWall = true;
                Instantiate(bossWall1, new Vector3(132.1f, 3.2f, -.5f), bossWall1.rotation);
                bossWall1.gameObject.name = "bossWall1Spawn";
                bossWall1.gameObject.tag = "BossWall1";
                extraWall = GameObject.FindGameObjectWithTag("ExtraWall");
                Destroy(extraWall.gameObject);
                extraWall2 = GameObject.FindGameObjectWithTag("ExtraWall2");
                Destroy(extraWall2.gameObject);
                bossTrigger = GameObject.FindGameObjectWithTag("BossTrigger");
                Destroy(bossTrigger.gameObject);
                fox.bossWall1 = GameObject.Find("bossWall1Spawn(Clone)");
                if (this.gameObject.transform.position.y < -14.42)
                {
                    floorChecker.isFloored = true;
                }
                //if (GameObject.Find("ExtraWall") != null)
                //{
                //    exWallsExist = true;
                //}
                //else
                //{
                //    exWallsExist = false;
                //    Debug.Log("no more walls");
                //}
                //while (exWallsExist == true)
                //{
                //    extraWall = GameObject.Find("ExtraWall");
                //    Destroy(extraWall.gameObject);
                //    Debug.Log("destroyed wall");
                //    extraWall = null;
                //    return;
                //}

            }
            if (SceneManager.GetActiveScene().name == "Level2")
            {
                activeWall = true;
                Instantiate(bossWall1, new Vector3(450f, -6.7f, 0f), bossWall1.rotation);
                bossWall1.gameObject.name = "bossWall1Spawn";
                //bossWall1.gameObject.tag = "BossWall1";
                extraWall = GameObject.FindGameObjectWithTag("ExtraWall");
                Destroy(extraWall.gameObject);
                extraWall2 = GameObject.FindGameObjectWithTag("ExtraWall2");
                Destroy(extraWall2.gameObject);
                bossTrigger = GameObject.FindGameObjectWithTag("BossTrigger");
                Destroy(bossTrigger.gameObject);
                fox.bossWall1 = GameObject.Find(("bossWall1Spawn(Clone)"));
                if (this.gameObject.transform.position.y < -24.65)
                {
                    floorChecker.isFloored = true;
                }
            }
            if (SceneManager.GetActiveScene().name == "Level3")
            {
                activeWall = true;
                Instantiate(bossWall1, new Vector3(32.32f, 105.22f, 0f), bossWall1.rotation);
                bossWall1.gameObject.name = "bossWall1Spawn";                
                //bossWall1.gameObject.tag = "BossWall1";
                extraWall = GameObject.FindGameObjectWithTag("ExtraWall");
                Destroy(extraWall.gameObject);
                extraWall2 = GameObject.FindGameObjectWithTag("ExtraWall2");
                Destroy(extraWall2.gameObject);
                bossTrigger = GameObject.FindGameObjectWithTag("BossTrigger");
                Destroy(bossTrigger.gameObject);
                finalFox.bossWall1 = GameObject.Find(("bossWall1Spawn(Clone)"));
                bossWall = finalFox.bossWall1;
                if (this.gameObject.transform.position.y < 87.328f)
                {
                    floorChecker.isFloored = true;
                }
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D rigid = GetComponent<Rigidbody2D>();

        //why did this work with on trigger with triangle dudes????
        //if you have a powerup and get hit, lose the powerup and gain temporary invincibility
        if (collision.gameObject.tag == "Enemy" && invincTime <= 0)
        {
            enemyCollide = true;
            Damage();
        }
        if (collision.gameObject.tag == "WaitEnemy" && invincTime <= 0)
        {
            enemyCollide = true;
            Damage();
        }

        if (collision.gameObject.name == "MovingPlatformH" || collision.gameObject.name == "WaitPlatformH" || collision.gameObject.name == "WaitPlatformHL" )
        {
            MovingPlatform movPlat = collision.gameObject.GetComponent<MovingPlatform>();
            movingPlat = collision.gameObject;
            platVel = collision.gameObject.GetComponent<Rigidbody2D>().velocity.x;
        }
        if (collision.gameObject.name == "MovingPlatformD" || collision.gameObject.name == "WaitPlatformD" | collision.gameObject.name == "MovingPlatformDR")
        {
            MovingPlatform movPlat = collision.gameObject.GetComponent<MovingPlatform>();
            movingPlat = collision.gameObject;
            platVel = collision.gameObject.GetComponent<Rigidbody2D>().velocity.x;
            platVel = (Mathf.Sqrt(2) * platVel);
            platVelY = collision.gameObject.GetComponent<Rigidbody2D>().velocity.y;
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        platVel = 0;
        platVelY = 0;
        movingPlat = null;
    }

    void Damage()
    {
        if (poweredUp)
        {
            poweredUp = false;
            healthNumber.text = "1";
            //isDoubleJump = false;
            invincTime = 1.2f;
            anim.SetBool("Invincible", true);
            return;
        }
        else
        {
            Death();
        }
    }

    void CheckPoint()
    {
        if (SceneManager.GetActiveScene().name == "Level1")
        {
            if (activeWall == true)
            {
                activeWall = false;
                bossWall = GameObject.Find("bossWall1Spawn(Clone)");
                if (bossWall != null)
                {
                    Destroy(bossWall.gameObject);
                }
                bossWall = GameObject.Find("BossWall(Clone)");
                if (bossWall != null)
                {
                    Destroy(bossWall.gameObject);
                }
                Instantiate(bossTriggerT, new Vector3(139.5f, -7.8f, 0), new Quaternion(0, 0, 0, 0));
                bossTriggerT.gameObject.tag = "BossTrigger";
                Instantiate(extraWallT, new Vector3(154f, -14.7f, 0), new Quaternion(0, 0, 0, 0));
                extraWallT.gameObject.tag = "ExtraWall";
                Instantiate(extraWall2T, new Vector3(149f, -14.7f, 0), new Quaternion(0, 0, 0, 0));
                extraWall2T.gameObject.tag = "ExtraWall2";
                fox.transform.position = new Vector3(151, -14);
                fox.health = 2;
            }

            //DJP check
            isDoubleJump = false;
            Instantiate(DoubleJumpPowerup, new Vector3(checkpointDJPx, checkpointDJPy, 0), new Quaternion(0, 0, 0, 0));
            checkpoint.GetComponent<Checkpoint>().DJP = DoubleJumpPowerup.gameObject;

        }
        if (SceneManager.GetActiveScene().name == "Level2")
        {
            if (activeWall == true)
            {
                activeWall = false;
                bossWall = GameObject.Find("bossWall1Spawn(Clone)");
                Destroy(bossWall.gameObject);
                Instantiate(bossTriggerT, new Vector3(455f, -17.5f, 0), new Quaternion(0, 0, 0, 0));
                bossTriggerT.gameObject.tag = "BossTrigger";
                Instantiate(extraWallT, new Vector3(468.02f, -24.73f, 0), new Quaternion(0, 0, 0, 0));
                extraWallT.gameObject.tag = "ExtraWall";
                Instantiate(extraWall2T, new Vector3(472.64f, -24.74f, 0), new Quaternion(0, 0, 0, 0));
                extraWall2T.gameObject.tag = "ExtraWall2";
                fox.transform.position = new Vector3(470.53f, -23.76f);
                fox.health = 2;
            }
            isDoubleJump = false;
            GameObject subwayTrigger = GameObject.Find("SubwayTrigger");
            if (subwayTrigger.GetComponent<SubwayTrigger>().trigger)
            {
                subwayTrigger.GetComponent<SubwayTrigger>().trigger = false;
                subwayTrigger.GetComponent<SubwayTrigger>().exists = false;
                if (GameObject.Find("SubwayCar(Clone)") != null)
                {
                    GameObject subwayCar = GameObject.Find("SubwayCar(Clone)");
                    Destroy(subwayCar.gameObject);
                }
            }       
            //DJP check
            Instantiate(DoubleJumpPowerup, new Vector3(checkpointDJPx, checkpointDJPy, 0), new Quaternion(0, 0, 0, 0));
            checkpoint.GetComponent<Checkpoint>().DJP = DoubleJumpPowerup.gameObject;
            //try
            //{
            //    checkpoint.GetComponent<Checkpoint>().DJP.name = checkpoint.GetComponent<Checkpoint>().DJP.name;
            //    Debug.Log("try");
            //}
            //catch (Exception ex)
            //{
            //    if (ex is UnassignedReferenceException)
            //    {
            //        none = true;
            //        Debug.Log("none");
            //    }
            //    if (ex is MissingReferenceException)
            //    {
            //        missing = true;
            //        Debug.Log("missing");
            //    }
            //}
            //if (!missing && !none)
            //{
            //    try
            //    {
            //        checkpoint.GetComponent<Checkpoint>().DJP.name = checkpoint.GetComponent<Checkpoint>().DJP.name;
            //    }
            //    catch (NullReferenceException)
            //    {
            //        NuLl = true;
            //        Debug.Log("null");
            //    }
            //}

            //if (!NuLl)
            //{
            //    if (missing)
            //    {
            //        Instantiate(DoubleJumpPowerup, new Vector3(checkpointDJPx, checkpointDJPy, 0), new Quaternion(0, 0, 0, 0));
            //        checkpoint.GetComponent<Checkpoint>().DJP = DoubleJumpPowerup.gameObject;
            //    }
            //}
            Destroy(GameObject.Find("WaitEnemies"));
            Instantiate(WaitEnemies, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
            Destroy(GameObject.Find("WaitPlatform"));
            Instantiate(WaitPlatforms, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
        }
        if (SceneManager.GetActiveScene().name == "Level3")
        {
            if (activeWall == true)
            {
                activeWall = false;
                bossWall = GameObject.FindGameObjectWithTag("BossWall1");
                Destroy(bossWall.gameObject);
                Instantiate(bossTriggerT, new Vector3(34.2f, 94.1f, 0), new Quaternion(0, 0, 0, 0));
                bossTriggerT.gameObject.tag = "BossTrigger";
                Instantiate(extraWallT, new Vector3(50.71f, 89.15f, 0), new Quaternion(0, 0, 0, 0));
                extraWallT.gameObject.tag = "ExtraWall";
                Instantiate(extraWall2T, new Vector3(55.81f, 89.2f, 0), new Quaternion(0, 0, 0, 0));
                extraWall2T.gameObject.tag = "ExtraWall2";
                if (collapse)
                {
                    Instantiate(CrumblePlatform, new Vector3(46.131f, 86.5f, 0), new Quaternion(0, 0, 0, 0));
                    collapse = false;
                    finalFox.phaseTwo = false;
                }
                finalFox.transform.position = new Vector3(53.03f, 88.19f);
                finalFox.health = 5;
            }
            isDoubleJump = false;

            if (checkpoint.gameObject.name == "CheckPoint1")
            {
                GameObject.Find("WaitPlatformV+2").gameObject.transform.position = new Vector2(-7.12f, 42.91f);
                GameObject.Find("WaitPlatformHL").gameObject.transform.position = new Vector2(-12.17f, 23.85f);
                GameObject.Find("WaitPlatformHL").GetComponent<WaitPlatform>().startPosX = -12.17f;
                GameObject.Find("WaitPlatformHL").GetComponent<WaitPlatform>().direction = false;
            }

            //LGP check
            Instantiate(LaserGunPowerup, new Vector3(checkpointLGPx, checkpointLGPy, 0), new Quaternion(0, 0, 0, 0));
            checkpoint.GetComponent<Checkpoint>().LGP = LaserGunPowerup.gameObject;
            //try
            //{
            //    checkpoint.GetComponent<Checkpoint>().LGP.name = checkpoint.GetComponent<Checkpoint>().LGP.name;
            //}

            //catch (Exception ex)
            //{
            //    if (ex is UnassignedReferenceException)
            //    {
            //        none = true;
            //    }
            //    if (ex is MissingReferenceException)
            //    {
            //        missing = true;
            //    }
            //}
            //if (!missing && !none)
            //{
            //    try
            //    {
            //        checkpoint.GetComponent<Checkpoint>().LGP.name = checkpoint.GetComponent<Checkpoint>().LGP.name;
            //    }
            //    catch (NullReferenceException)
            //    {
            //        NuLl = true;
            //    }
            //}

            //if (!NuLl)
            //{
            //    if (missing)
            //    {
            //        Instantiate(LaserGunPowerup, new Vector3(checkpointLGPx, checkpointLGPy, 0), new Quaternion(0, 0, 0, 0));
            //        checkpoint.GetComponent<Checkpoint>().LGP = LaserGunPowerup.gameObject;
            //    }
            //}
        }
        Time.timeScale = 0;
        gameObject.transform.position = checkpoint.transform.position;
        healthNumber.text = "1";
        vel = new Vector2(0, 0);
        Time.timeScale = 1;
        PlayerPrefs.SetInt("lives", seedHandler.numberOfLives - 1);
    }

    void Death()
    {
        healthNumber.text = "0";
        if (seedHandler.numberOfLives >= 1)
        {
            if (checkpoint != null && checkpoint.GetComponent<Checkpoint>().checkpoint == true)
            {
                CheckPoint();
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                //seedHandler.numberOfLives = seedHandler.numberOfLives - 1;
                PlayerPrefs.SetInt("lives", seedHandler.numberOfLives - 1);
                return;
            }
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            seedHandler.numberOfLives = 6;
            PlayerPrefs.SetInt("lives", seedHandler.numberOfLives);
            return;
        }
    }

    void Lives()
    {
        if (PlayerPrefs.HasKey("lives"))
        {
            seedHandler.numberOfLives = PlayerPrefs.GetInt("lives");
            seedHandler.totalLives = PlayerPrefs.GetInt("lives");
            return;
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    void FixedUpdate()
    {
        Rigidbody2D rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = vel;
        rigid.AddForce(jumpForce);
    }
}
