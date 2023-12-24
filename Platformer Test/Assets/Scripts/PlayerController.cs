using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed = 2.0f;
    public Vector2 vel;
    public float jumpHeight = 100f;
    public Vector2 jumpForce;
    public FloorChecker floorChecker;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //if (Input.GetKey(KeyCode.A))
        //{
        //    Vector3 newPos = transform.position;
        //    newPos.x -= speed * Time.deltaTime;
        //    transform.position = newPos;
        //}
        //if (Input.GetKey(KeyCode.D))
        //{
        //    Vector3 newPos = transform.position;
        //    newPos.x += speed * Time.deltaTime;
        //    transform.position = newPos;
        //}


        //Vector3 newPos = transform.position;
        //newPos.x += speed * Time.deltaTime * Input.GetAxis("Horizontal");
        //transform.position = newPos;
        Rigidbody2D rigid = GetComponent<Rigidbody2D>();
        vel = new Vector2(Input.GetAxis("Horizontal") * speed, rigid.velocity.y);

        jumpForce = new Vector2(0, 0);
        if(Input.GetKeyDown(KeyCode.Space) && floorChecker.isFloored)
            jumpForce = new Vector2(0, jumpHeight);
    }
    void FixedUpdate()
    {
        Rigidbody2D rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = vel;
        rigid.AddForce(jumpForce);
        
    }
}
