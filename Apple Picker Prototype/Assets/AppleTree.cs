﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets
{
    public class AppleTree : MonoBehaviour { 
        [Header("Set in Inspector")]
        // Prefab for instantiating apples
        public GameObject applePrefab;

        // Speed at which the AppleTree moves
        public float speed = 1f;

        // Distance where AppleTree turns around
        public float leftAndRightEdge = 10f;

        // Chance that the AppleTree will change directions
        public float chanceToChangeDirections = 0.1f;

        // Rate at which Apples will be instantiated
        public float secondsBetweenAppleDrops = 1f;

        void Start()
        {
            //Dropping apples every second
            Invoke("DropApple", 2f);
        }

        void DropApple()
        {
            GameObject apple = Instantiate<GameObject>(applePrefab);
            apple.transform.position = transform.position;
            Invoke("DropApple", secondsBetweenAppleDrops); 
        }
        void Update()
        {
            //Basic Movement
            Vector3 pos = transform.position;
            pos.x += speed * Time.deltaTime;
            transform.position = pos;

            // Changing Directions
            if ( pos.x < -leftAndRightEdge)
            {
                speed = Mathf.Abs(speed); //Move Right
            } else if ( pos.x > leftAndRightEdge)
            {
                speed = -Mathf.Abs(speed); //Move Left
            } 
        }
        void FixedUpdate()
        {
         // Changing Direction Randomly is now time-based because of FixedUpdate()
         if (UnityEngine.Random.value < chanceToChangeDirections)
            {
                speed *= -1; // Change direction
            }   
        }
    }
    
    }

