using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Completion : MonoBehaviour {

    public Transform completion;
    public GameObject ComPletion;
    public Canvas canvas;

	// Use this for initialization
	void Start () {
        if (PlayerPrefs.GetInt("lvl1Egg") == 1 && PlayerPrefs.GetInt("lvl2Egg") == 1 && PlayerPrefs.GetInt("lvl3Egg") == 1)
        {
            ComPletion.SetActive(true);
            //var CompletionObject = Instantiate(completion, new Vector3(this.transform.position.x ,this.transform.position.y - 124.5f,0), completion.rotation);
            //CompletionObject.transform.SetParent(canvas.transform);
            //CompletionObject.transform.position = new Vector3(-512, -384, 0);
        }
    }
	
	// Update is called once per frame
	void Update () {
        
		
	}
}
