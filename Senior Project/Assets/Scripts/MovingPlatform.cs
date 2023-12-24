using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

    public GameObject movPlat;
    public Vector2 vel;
    //public System.Random rnd = new System.Random();
    public float startPosX;
    public float startPosY;
    public float endPosX;
    public float endPosY;
    public float speed = 2.0f;
    //direction is left when false, right when true
    public bool direction;

    // Use this for initialization
    void Start()
    {
        startPosX = movPlat.transform.position.x;
        startPosY = movPlat.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        Rigidbody2D rigid = GetComponent<Rigidbody2D>();

        if (direction == false)
        {
            if (movPlat.gameObject.name == "MovingPlatformH")
            {
                vel = new Vector2(-speed, rigid.velocity.y);
            }
            if (movPlat.gameObject.name == "MovingPlatformD")
            {
                vel = new Vector2(-(Mathf.Sqrt(2) * speed) / 2, (Mathf.Sqrt(2) * speed) / 2);
            }
            if (movPlat.gameObject.name == "MovingPlatformDR")
            {
                vel = new Vector2(-(Mathf.Sqrt(2) * speed) / 2, -(Mathf.Sqrt(2) * speed) / 2);
            }
            if (movPlat.gameObject.name == "MovingPlatformV")
            {
                vel = new Vector2(rigid.velocity.x, speed);
            }

        }
        if (direction == true)
        {
            if (movPlat.gameObject.name == "MovingPlatformH")
            {
                vel = new Vector2(speed, rigid.velocity.y);
            }
            if (movPlat.gameObject.name == "MovingPlatformD")
            {
                vel = new Vector2((Mathf.Sqrt(2) * speed) / 2, -(Mathf.Sqrt(2) * speed) / 2);
            }
            if (movPlat.gameObject.name == "MovingPlatformDR")
            {
                vel = new Vector2((Mathf.Sqrt(2) * speed) / 2, (Mathf.Sqrt(2) * speed) / 2);
            }
            if (movPlat.gameObject.name == "MovingPlatformV")
            {
                vel = new Vector2(rigid.velocity.x, -speed);
            }
        }

        //changing direction
        if (movPlat.gameObject.name == "MovingPlatformH")
        {
            if (movPlat.transform.position.x > startPosX + endPosX)
            {
            direction = false;
            }
            if (movPlat.transform.position.x < startPosX - endPosX)
            {
            direction = true;
            }
        }
        if (movPlat.gameObject.name == "MovingPlatformD" || movPlat.gameObject.name == "MovingPlatformDR")
        {
            if (movPlat.transform.position.x > startPosX + (Mathf.Sqrt(2) *endPosX) / 2 )
            {
                direction = false;
            }
            if (movPlat.transform.position.x < startPosX - (Mathf.Sqrt(2) * endPosX) / 2)
            {
                direction = true;
            }
        }
        if (this.gameObject.name == "MovingPlatformV")
        {
            
            if (movPlat.transform.position.y > startPosY)
            {
                direction = true;
            }
            if (movPlat.transform.position.y <= startPosY - endPosY)
            {
                direction = false;
            }
        }
    }

    void FixedUpdate()
    {
        Rigidbody2D rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = vel;
    }
}
