using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class Checkpoint : MonoBehaviour {

    Animator anim;
    public bool checkpoint = false;
    public GameObject DJP;
    public GameObject LGP;
    public float LGPx;
    public float LGPy;
    public float DJPx;
    public float DJPy;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        LGPx = -10000;
        LGPy = -10000;
        if (LGP != null)
        {
            LGPx = LGP.gameObject.transform.position.x;
            LGPy = LGP.gameObject.transform.position.y;
        }
        if (DJP != null)
        {
            DJPx = DJP.gameObject.transform.position.x;
            DJPy = DJP.gameObject.transform.position.y;
        }
    }
	
	//public void Save()
 //   {
 //       BinaryFormatter bf = new BinaryFormatter();
 //       FileStream file = File.Open(Application.persistentDataPath + "/checkPoint.dat", FileMode.Open);

 //       CheckPointData data = new CheckPointData();

 //       bf.Serialize(file, data);
 //       file.Close();
 //   }

 //   public void Load()
 //   {
 //       if(File.Exists(Application.persistentDataPath + "/checkPoint.dat"))
 //       {
 //           BinaryFormatter bf = new BinaryFormatter();
 //           FileStream file = File.Open(Application.persistentDataPath + "/checkPoint.dat");
 //           CheckPointData data = (CheckPointData)bf.Deserialize(file);
 //           file.Close();
 //       }
 //   }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && checkpoint == false)
        {
            checkpoint = true;
            anim.SetBool("Checkpoint", checkpoint);
        }
    }
}

//[Serializable]
//class CheckPointData
//{

//}
