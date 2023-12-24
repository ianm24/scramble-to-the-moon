using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubwayEnd : MonoBehaviour {

    public GameObject subwayTrigger;

	// Use this for initialization
	void Start () {
        subwayTrigger = GameObject.Find("SubwayTrigger");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            subwayTrigger.GetComponent<SubwayTrigger>().trigger = false;
        }
    }
}
