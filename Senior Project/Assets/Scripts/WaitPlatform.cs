using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitPlatform : MonoBehaviour {

    public GameObject movPlat;
    public Vector2 vel;
    public float startPosX;
    public float startPosY;
    public float endPosX;
    public float endPosY;
    public float speed = 2.0f;
    //direction is left when false, right when true
    public bool direction;
    public bool riding;
    

    // Use this for initialization
    void Start () {
        startPosX = movPlat.transform.position.x;
        startPosY = movPlat.transform.position.y;
    }
	
	// Update is called once per frame
	void Update ()
    {
        Rigidbody2D rigid = GetComponent<Rigidbody2D>();

        if (riding == true && GameObject.Find("Player").gameObject.transform.position.y > this.transform.position.y + .5f && this.gameObject.name != "WaitPlatformV")
        {
            if (this.gameObject.name == "WaitPlatformH")
            {
                vel = new Vector2(speed, rigid.velocity.y);
                if (movPlat.transform.position.x > startPosX + endPosX)
                {
                    vel = new Vector2(0, rigid.velocity.y);
                    riding = false;
                }
            }
            if (this.gameObject.name == "WaitPlatformHL")
            {
                if (direction == false)
                {
                    vel = new Vector2(-speed, rigid.velocity.y);
                    if (movPlat.transform.position.x < startPosX - endPosX)
                    {
                        riding = false;
                        startPosX = movPlat.transform.position.x;
                        startPosY = movPlat.transform.position.y;
                        direction = true;
                        vel = new Vector2(0, rigid.velocity.y);
                    }
                }
                if (direction)
                {
                    vel = new Vector2(speed, rigid.velocity.y);
                    if (movPlat.transform.position.x > startPosX + endPosX)
                    {
                        riding = false;
                        startPosX = movPlat.transform.position.x;
                        startPosY = movPlat.transform.position.y;
                        direction = false;
                        vel = new Vector2(0, rigid.velocity.y);
                    }
                }
            }
            if (this.gameObject.name == "WaitPlatformD")
            {
                vel = new Vector2((Mathf.Sqrt(2) * speed) / 2, -(Mathf.Sqrt(2) * speed) / 2);
                if (movPlat.transform.position.x > startPosX + (Mathf.Sqrt(2) * endPosX) / 2)
                {
                    vel = new Vector2(0, 0);
                    riding = false;
                }
            }
            if (this.gameObject.name == "WaitPlatformV-")
            {
                vel = new Vector2(rigid.velocity.x, -speed);
                if (movPlat.transform.position.y < startPosY - endPosY)
                {
                    vel = new Vector2(0,0);
                    riding = false;
                }
            }
            if (this.gameObject.name == "WaitPlatformV+" || this.gameObject.name == "WaitPlatformV+1" || this.gameObject.name == "WaitPlatformV+2")
            {
                vel = new Vector2(rigid.velocity.x, speed);
                if (movPlat.transform.position.y > startPosY + endPosY)
                {
                    vel = new Vector2(0, 0);
                    riding = false;
                }
            }
            //if (this.gameObject.name == "WaitPlatformV")
            //{
            //    if (direction)
            //    {
            //        vel = new Vector2(rigid.velocity.x, -speed);
            //    }
            //    if (direction == false)
            //    {
            //        vel = new Vector2(rigid.velocity.x, speed);
            //    }
            //    if (movPlat.transform.position.y > startPosY)
            //    {
            //        direction = true;
            //    }
            //    if (movPlat.transform.position.y < startPosY - endPosY)
            //    {
            //        direction = false;
            //    }
            //}
        }
        else
        {
            vel = new Vector2(0, 0);
        }
        if (riding == true  && this.gameObject.name == "WaitPlatformV")
        {
            if (direction)
            {
                vel = new Vector2(rigid.velocity.x, -speed);
            }
            if (direction == false)
            {
                vel = new Vector2(rigid.velocity.x, speed);
            }
            if (movPlat.transform.position.y > startPosY)
            {
                direction = true;
            }
            if (movPlat.transform.position.y < startPosY - endPosY)
            {
                direction = false;
            }
        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            riding = true;
        }
    }

    void FixedUpdate()
    {
        Rigidbody2D rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = vel;
    }
}
