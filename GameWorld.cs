using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWorld : MonoBehaviour
{
    
    private Vector2 nextDestination;
    private bool autoReturnControl;
    
    // game states
    public string initState = "intro";
    
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);        
    }

    public void setNextDestination(float x, float y)
    {
        nextDestination = new Vector2(x, y);
    }

    public void setAutoReturnControl(bool setting)
    {
        autoReturnControl = setting;
    }

    public bool getAutoReturnControl()
    {
        return autoReturnControl;
    }

    public Vector2 getNextDestination()
    {
        return nextDestination;
    }
}
