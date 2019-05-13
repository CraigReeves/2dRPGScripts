using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWorld : MonoBehaviour
{
    
    private Vector2 nextDestination;
    private bool autoReturnControl;
    private List<Treasure> obtainedTreasures;
    public Hashtable gameState;
        
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);      
        obtainedTreasures = new List<Treasure>();
        gameState = new Hashtable();
    }

    public List<Treasure> getObtainedTreasures()
    {
        return obtainedTreasures;
    }
    
    public void addTreasure(Treasure treasure)
    {
        obtainedTreasures.Add(treasure);
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
