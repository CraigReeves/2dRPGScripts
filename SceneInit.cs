using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneInit : MonoBehaviour
{

    private PlayerMovement player;
    private CameraController camera;
    private GameWorld gameWorld;
    private Animator faderAnim;
    private Weather weather;
    
    private void OnEnable()
    {
        gameWorld = FindObjectOfType<GameWorld>();
        player = FindObjectOfType<PlayerMovement>();
        faderAnim = GameObject.Find("FaderImage").GetComponent<Animator>();
        weather = FindObjectOfType<Weather>();
        
        // position player
        var nextDestination = gameWorld.getNextDestination();

        if (nextDestination.x != 0 && nextDestination.y != 0)
        {
            player.transform.position = new Vector3(nextDestination.x, nextDestination.y);
        }

        weather.rainByDefault = gameWorld.nextRain;
        weather.snowByDefault = gameWorld.nextSnow;
        weather.fogByDefault = gameWorld.nextFog;
        weather.sceneDarkenedByDefault = gameWorld.nextDarkness;
        
        camera = FindObjectOfType<CameraController>();

        camera.setFollowTarget(player.gameObject);
        
        faderAnim.SetTrigger("fade_in");
    }
}
