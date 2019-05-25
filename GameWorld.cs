using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWorld : MonoBehaviour
{
    
    private Vector2 nextDestination;
    public bool nextRain;
    public bool nextSnow;
    public bool nextFog;
    public bool nextDarkness;
    private bool autoReturnControl;
    private List<Treasure> obtainedTreasures;
    public Hashtable gameState;
    public PlayerMovement[] party;
    
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);      
        obtainedTreasures = new List<Treasure>();
        gameState = new Hashtable(); 
        
        // Initialize party
        initializeParty();
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.K))
        {
            // set next player in control
            cyclePartyMembers();
        }
    }

    private void initializeParty()
    {
        // remove control from all but the first player
        for (var i = 0; i < party.Length; i++)
        {
            if (i == 0)
            {
                party[i].setIsFollowing(false);
                party[i].setControlOverride(false);
                continue;
            } 
                
            party[i].setIsFollowing(true);
            party[i].setControlOverride(true);
            party[i].setFollowTarget(party[i - 1]);
        }
    }

    public PlayerMovement partyLeader()
    {
        return party[0]; 
    }
    
    public void cyclePartyMembers()
    {
        var newOrder = new PlayerMovement[party.Length];
        
        var partyPositions = new Vector3[party.Length];
        
        for (var i = 0; i < party.Length; i++)
        {
            party[i].setIsFollowing(false);
            party[i].setControlOverride(true);
            
            // get old party positions
            partyPositions[i].x = party[i].transform.position.x;
            partyPositions[i].y = party[i].transform.position.y;
            partyPositions[i].z = party[i].transform.position.z;
            
            if (i == 0)
            {
                newOrder[newOrder.Length - 1] = party[i];
                continue;
            }

            newOrder[i - 1] = party[i];
            
        }

        party = newOrder;
        
        // reposition party
        for (var i = 0; i < party.Length; i++)
        {
            party[i].transform.position = partyPositions[i];
        }
        
        initializeParty();
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
