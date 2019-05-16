using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoScrollBGVertical : MonoBehaviour
{
    private Vector3 startPOS;
    public float speed;
    public Transform background1;
    public Transform background2;
    private float differencePos;
    private float orig1Pos;
    private float orig2Pos;
    private float loopBackPos;
    public bool scrollBackwards;
    
    // Start is called before the first frame update
    void Start()
    {
        differencePos = background2.position.y - background1.position.y;
        orig1Pos = background1.position.y;
        orig2Pos = background2.position.y;

        if (!scrollBackwards)
        {
            loopBackPos = orig1Pos - differencePos;
        }
        else
        {
            loopBackPos = orig2Pos + differencePos;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!scrollBackwards)
        {
            background1.Translate((new Vector3(0, -1f, 0)) * speed * Time.deltaTime);
            background2.Translate((new Vector3(0, -1f, 0)) * speed * Time.deltaTime);
            
            if (background1.position.y < loopBackPos)
            {
                var b1ypos = background2.position.y + differencePos;
                background1.position = new Vector3(background1.position.x, b1ypos, background1.position.z);
            }
        
            if (background2.position.y < loopBackPos)
            {
                var b2ypos = background1.position.y + differencePos;
                background2.position = new Vector3(background1.position.x, b2ypos, background2.position.z);
            }
        }
        else
        {
            background1.Translate((new Vector3(0, 1f, 0)) * speed * Time.deltaTime);
            background2.Translate((new Vector3(0, 1f, 0)) * speed * Time.deltaTime);
            
            if (background2.position.y > loopBackPos)
            {
                var b2ypos = background1.position.y - differencePos;
                background2.position = new Vector3(background2.position.x, b2ypos, background2.position.z);
            }
        
            if (background1.position.y > loopBackPos)
            {
                var b1ypos = background2.position.y - differencePos;
                background1.position = new Vector3(background1.position.x, b1ypos, background1.position.z);
            }
        }           
    }
}
