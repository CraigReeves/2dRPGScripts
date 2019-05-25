using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneInit : MonoBehaviour
{

    private CameraController camera;
    private GameWorld gameWorld;
    private Animator faderAnim;
    private Weather weather;
    
    private void OnEnable()
    {
        gameWorld = FindObjectOfType<GameWorld>();
        faderAnim = GameObject.Find("FaderImage").GetComponent<Animator>();
        weather = FindObjectOfType<Weather>();
        
        // position player
        var nextDestination = gameWorld.getNextDestination();

        if (nextDestination.x != 0 && nextDestination.y != 0)
        {
            gameWorld.partyLeader().transform.position = new Vector3(nextDestination.x, nextDestination.y);
            
            // position remaining party members
            if (gameWorld.party.Length > 1)
            {
                foreach (var player in gameWorld.party)
                {
                    player.transform.position = gameWorld.partyLeader().transform.position;
                }
            }
        }

        weather.rainByDefault = gameWorld.nextRain;
        weather.snowByDefault = gameWorld.nextSnow;
        weather.fogByDefault = gameWorld.nextFog;
        weather.sceneDarkenedByDefault = gameWorld.nextDarkness;
        
        camera = FindObjectOfType<CameraController>();

        camera.setFollowTarget(gameWorld.partyLeader().gameObject);
        
        faderAnim.SetTrigger("fade_in");
    }
}
