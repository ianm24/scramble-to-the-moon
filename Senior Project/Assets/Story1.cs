using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Story1 : MonoBehaviour {

    public Animator anim;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Transition"))
        {
            if (SceneManager.GetActiveScene().name == "Story1")
            {
                SceneManager.LoadScene("Level1");
            }
            if (SceneManager.GetActiveScene().name == "Story2")
            {
                SceneManager.LoadScene("Level2");
            }
            if (SceneManager.GetActiveScene().name == "Story3")
            {
                SceneManager.LoadScene("Level3");
            }
            if (SceneManager.GetActiveScene().name == "Story4")
            {
                SceneManager.LoadScene("Credits");
            }
        }
    }
}
