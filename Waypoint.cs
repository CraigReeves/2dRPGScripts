using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Waypoint : MonoBehaviour
{
    public Scene levelToLoad;
    public Vector2 position;
    protected GoToScene goToScene;
    private GameWorld gameWorld;
    
    // Start is called before the first frame update
    protected void Start()
    {
        goToScene = FindObjectOfType<GoToScene>();
        gameWorld = FindObjectOfType<GameWorld>();
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (goToScene.getPlayer().isUnderPlayerControl())
        {
            goToScene.setPosition(position.x, position.y);
            goToScene.setLevelToLoad(levelToLoad);
            gameWorld.setAutoReturnControl(true);
            
            goToScene.go();
        }
    }
}
