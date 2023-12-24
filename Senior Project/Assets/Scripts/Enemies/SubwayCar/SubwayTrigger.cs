using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubwayTrigger : MonoBehaviour {

    public bool trigger;
    public bool exists = false;
    public float timer;
    public Transform subwayCar;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (trigger && exists == false && timer <= 0)
        {
            Instantiate(subwayCar, new Vector2(403f, -49), new Quaternion(0, 0, 0, 0));
            exists = true;
            return;
        }
        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }
	}

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            trigger = true;
        }
    }
}
