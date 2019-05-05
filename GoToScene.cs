using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToScene : MonoBehaviour
{
    private CharacterMovement character;
    private Scene levelToLoad;
    private Vector2 position;
    private GameWorld gameWorld;
    private Animator faderAnim;
    private bool isEvent;
    
    protected void Start()
    {
        gameWorld = FindObjectOfType<GameWorld>();
        faderAnim = GameObject.Find("FaderImage").GetComponent<Animator>();
        
        // assign a CM if one isn't already there
        if (character == null)
        {
            character = FindObjectOfType<PlayerMovement>();
        }
    }
    
    public void go()
    {
        if (character.isUnderPlayerControl() || isEvent)
        {
            character.setControlOverride(true);
            faderAnim.SetTrigger("fade_out");
            character.clearDirectionalBuffer();
            
            gameWorld.setNextDestination(position.x, position.y);         
        }
    }

    public void setAsEvent(bool setting)
    {
        isEvent = setting;
    }

    public void setLevelToLoad(Scene levelToLoad)
    {
        this.levelToLoad = levelToLoad;
    }

    public void setPosition(float x, float y)
    {
        position = new Vector2(x, y);
    }

    public void setPlayer(CharacterMovement cm)
    {
        character = cm;
    }

    public CharacterMovement getPlayer()
    {
        return character;
    }

    public void fadeInComplete()
    {
        if (gameWorld.getAutoReturnControl())
        {
            character.setControlOverride(false);  
        }
    }

    public void loadScene()
    {
        character.setControlOverride(true);
        SceneManager.LoadScene(levelToLoad.handle);
    }
}
