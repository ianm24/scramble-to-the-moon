using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEnemy : MonoBehaviour {

    public GameObject movEnem;
    public GameObject platForm;
    public GameObject player;
    public Projectile proj;
    public Transform projectileV;
    public Transform projectileH;
    public DeathChecker deathChecker;
    public Vector2 vel;
    Animator anim;
    public float startPos;
    public float startY;
    public float endPos;
    public float endY;
    public float playerRange;
    public float speed;
    public float timer;
    public float timerMax;
    public float deathTimer;
    public float projTime = 3;
    public float platVel;
    //direction is left when false, right when true
    public bool direction;
    public bool upDown;
    public bool onPlat;
    public bool playerInRange;
    public bool shot;

    // Use this for initialization
    void Start ()
    {
        startPos = movEnem.transform.position.x;
        startY = movEnem.transform.position.y;
        if (this.gameObject.name != "UFOEnemy")
        {
            endY = 15;
        }        
        player = GameObject.Find("Player");
        deathTimer = .15f;
        shot = false;
        if (this.gameObject.name != "WaitEnemy")
        {
            playerInRange = true;                
        }
    }

    // Update is called once per frame
    void Update()
    {
        Rigidbody2D rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        //movement
        if (direction == false && playerInRange)
        {
            vel = new Vector2(-speed, rigid.velocity.y);
            if (anim != null)
            {
                anim.SetFloat("speed", speed);
                anim.SetBool("Direction", direction);
            }            
        }
        if (direction == true && playerInRange)
        {
            vel = new Vector2(speed, rigid.velocity.y);
            if (anim != null)
            {
                anim.SetFloat("speed", -speed);
                anim.SetBool("Direction", direction);
            }
        }
        if (movEnem.transform.position.x > startPos + endPos)
        {
            direction = false;
        }
        if (movEnem.transform.position.x < startPos - endPos)
        {
            direction = true;
        }

        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        if (this.gameObject.name == "UFOEnemy")
        {
            if (upDown)
            {
                vel = new Vector2(rigid.velocity.x, -speed);
            }
            if (upDown == false)
            {
                vel = new Vector2(rigid.velocity.x, speed);
            }
            if (this.transform.position.y > startY)
            {
                upDown = true;
            }
            if (this.transform.position.y < startY - endY)
            {
               upDown = false;
            }
        }

        //drops projectiles if close to player
        if (gameObject.transform.position.x <= player.transform.position.x + playerRange && gameObject.transform.position.x >= player.transform.position.x - playerRange && timer <= 0)
        {

            if (this.gameObject.name == "CrowEnemy")
            {
                Invoke("DropProjectile", 0);
                timer = 1f;
            }
            if (this.gameObject.name == "ShootEnemy" || this.gameObject.name == "UFOEnemy")
            {
                Invoke("ShootProjectile", 0);
                timer = timerMax;
            }
            if (this.gameObject.name == "WaitEnemy" && player.gameObject.transform.position.y < this.gameObject.transform.position.y +5)
            {
                playerInRange = true;
                this.GetComponent<Animator>().enabled = true;
            }
            
        }

        //if (this.gameObject.transform.position.y < startY - endY - 1)
        //{
        //    Destroy(this.gameObject);
        //}

        if (onPlat)
        {
            if (platForm.GetComponent<MovingPlatform>().direction == true)
            {
                direction = true;
            }
            if (platForm.GetComponent<MovingPlatform>().direction == false)
            {
                direction = false;
            }
        }

        if (deathChecker != null)
        {
            if (deathChecker.isDead == true)
            {
                deathTimer -= Time.deltaTime;
            }
            if (deathTimer <= 0)
            {
                Destroy(this.gameObject);
            }
        }

        if (shot)
        {
            Destroy(this.gameObject);
        }
    }

    void DropProjectile()
    {
        Rigidbody2D rigid = GetComponent<Rigidbody2D>(); 
        Instantiate(projectileV, new Vector2(transform.position.x,transform.position.y + .1f), new Quaternion(0,0,0,0));
        projectileV.gameObject.name = "Projectile";
        projectileV.GetComponent<Projectile>().host = this.gameObject;
        projectileH.GetComponent<Projectile>().vel = new Vector2(0, -1.5f);
    }

    void ShootProjectile()
    {
        Rigidbody2D rigid = GetComponent<Rigidbody2D>();
        Instantiate(projectileH, new Vector2(this.transform.position.x, transform.position.y + .1f), new Quaternion(0, 0, 0, 0));
        projectileH.GetComponent<Projectile>().host = this.gameObject;
        projectileH.gameObject.name = "Projectile";
        
        if (direction)
        {
            projectileH.GetComponent<Projectile>().vel = new Vector2(1.5f * 2, 0);
        }
        if (!direction)
        {            
            projectileH.GetComponent<Projectile>().vel = new Vector2(1.5f * -2, 0);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D rigid = GetComponent<Rigidbody2D>();

        if (collision.gameObject.name == "MovingPlatformD" || collision.gameObject.name == "MovingPlatformH")
        {
            onPlat = true;
            platVel = collision.rigidbody.velocity.x;
            platForm = collision.gameObject;
        } 
        if (collision.gameObject.name == "WalkEnemy")
        {
            direction = !direction;
        }
        if (collision.gameObject.tag == "LGPProjectile")
        {
            shot = true;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "LGPProjectile")
        {
            shot = true;
        }
    }

    void FixedUpdate()
    {
        Rigidbody2D rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = vel;
    }
}
