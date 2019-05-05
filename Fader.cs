using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fader : MonoBehaviour
{
    public GoToScene goToSceneScript;

    private void Start()
    {
        goToSceneScript = FindObjectOfType<GoToScene>();
    }

    public void OnFadeOutComplete()
    {
        goToSceneScript.loadScene();
    }

    public void OnFadeInComplete()
    {
        goToSceneScript.fadeInComplete();
    }
}
