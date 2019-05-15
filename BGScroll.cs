﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroll : MonoBehaviour
{
    private Vector3 startPOS;
    public float speed;
    public Transform background1;
    public Transform background2;
    private float differencePos;
    private float orig1Pos;
    private float loopBackPos;
    
    // Start is called before the first frame update
    void Start()
    {
        differencePos = background2.position.x - background1.position.x;
        orig1Pos = background1.position.x;
        loopBackPos = orig1Pos - (differencePos + differencePos / 2);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        background1.Translate((new Vector3(-1, 0, 0)) * speed * Time.deltaTime);
        background2.Translate((new Vector3(-1, 0, 0)) * speed * Time.deltaTime);
        
        if (background1.position.x < loopBackPos)
        {
            var b1xpos = background2.position.x + differencePos;
            background1.position = new Vector3(b1xpos, background1.position.y, background1.position.z);
        }
        
        if (background2.position.x < loopBackPos)
        {
            var b2xpos = background1.position.x + differencePos;
            background2.position = new Vector3(b2xpos, background2.position.y, background2.position.z);
        }
    }
}
