using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubwayCar : MonoBehaviour {

    public Vector2 vel;
    public float startPos;
    public float endPos;

    // Use this for initialization
    void Start ()
    {
        vel = new Vector2(-15, 0);
        startPos = this.transform.position.x;
	}
	
	// Update is called once per frame
	void Update ()
    {
        GameObject subwayTrigger = GameObject.Find("SubwayTrigger");

        if (this.transform.position.x < startPos - endPos)
        {
            subwayTrigger.GetComponent<SubwayTrigger>().exists = false;
            subwayTrigger.GetComponent<SubwayTrigger>().timer = 10f;
            Destroy(this.gameObject);
        }

        GameObject playerController = GameObject.Find("Player");
        if (this.gameObject.transform.position.x < playerController.transform.position.x + 2*15)
        {
            this.GetComponent<AudioSource>().enabled = true;
            return;
        }
    }

    void FixedUpdate()
    {
        Rigidbody2D rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = vel;
    }
}
