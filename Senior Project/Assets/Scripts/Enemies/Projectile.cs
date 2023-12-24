using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Projectile : MonoBehaviour {

    public GameObject host;
    public Vector2 vel;
    public bool collide = false;
    public bool error = false;
    public float startPos;
    public float currentPos;
    public float aliveTimer;
    public float maxTime;
    Animator anim;

    void Start()
    {
        startPos = this.gameObject.transform.position.x;
        aliveTimer = 0;
        if (host != null)
        {
            maxTime = host.GetComponent<MovingEnemy>().projTime;
        }
        anim = GetComponent<Animator>();
        anim.SetFloat("speed", vel.x);
    }

    // Update is called once per frame
    void Update () {
        Rigidbody2D rigid = GetComponent<Rigidbody2D>();
        aliveTimer += Time.deltaTime;

        if (host != null)
        {
            if (host.gameObject.name != "ShootEnemy" || host.gameObject.name != "CrowEnemy")
            {
                try
                {
                    host.GetComponent<Rigidbody2D>();
                }
                catch (NullReferenceException)
                {
                    error = true;
                    return;
                }
                try
                {
                    host.GetComponent<Rigidbody2D>();
                }
                catch (MissingReferenceException)
                {
                    error = true;
                }
            }
        }
        else
        {
            error = true;
        }
        
        

        if (error || collide || aliveTimer > maxTime /*|| transform.position.y < host.transform.position.y - 5 || transform.position.x > host.transform.position.x + 10 || transform.position.x < host.transform.position.x - 10*/ )
        {
            Destroy(this.gameObject);
        }
        vel = new Vector2(rigid.velocity.x, -3f);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Enemy" && collision.gameObject.tag != "DeathChecker")
        {
            collide = true;
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Enemy" && collision.gameObject.tag != "DeathChecker")
        {
            collide = true;
        }
    }

    void FixedUpdate()
    {
        Rigidbody2D rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = vel;
    }
}
