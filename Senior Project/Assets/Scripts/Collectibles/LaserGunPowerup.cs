using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGunPowerup : MonoBehaviour {

    public Vector2 vel;
    public GameObject Player;

    // Use this for initialization
    void Start () {
        Player = GameObject.Find("Player");
        if (Player.GetComponent<PlayerController>().direction)
        {
            vel = new Vector2(14, 0);
        }
        if (Player.GetComponent<PlayerController>().direction == false)
        {
            vel = new Vector2(-14, 0);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name != "Player" )
        {
            Destroy(this.gameObject);
        }
    }

    void FixedUpdate()
    {
        Rigidbody2D rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = vel;
    }
}
