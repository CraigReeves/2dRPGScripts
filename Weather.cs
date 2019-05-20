using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Weather : MonoBehaviour
{
    private ParticleSystem rain;
    private ParticleSystem snow;
    private ParticleSystem fog;
    public Tilemap[] tilemaps;
    public bool rainByDefault;
    public bool snowByDefault;
    public bool fogByDefault;
    public bool sceneDarkenedByDefault;
    
    // Start is called before the first frame update
    void Start()
    {
        rain = GameObject.Find("RainGenerator").GetComponent<ParticleSystem>();
        snow = GameObject.Find("SnowGenerator").GetComponent<ParticleSystem>();
        fog = GameObject.Find("FogGenerator").GetComponent<ParticleSystem>();
        
        // disable by default
        rain.gameObject.SetActive(rainByDefault);
        snow.gameObject.SetActive(snowByDefault);
        fog.gameObject.SetActive(fogByDefault);  
        
        // screen darkened by default
        if (sceneDarkenedByDefault)
        {
            foreach (var tilemap in tilemaps)
            {
                tilemap.color = Color.gray;
            }
        }
    }
    
    // enables rain
    public void setRain(bool setting, bool darkenScene)
    {
        if (darkenScene)
        {
            foreach (var tilemap in tilemaps)
            {
                tilemap.color = Color.gray;
            }
        }

        rain.gameObject.SetActive(setting);
    }
    
    public void setSnow(bool setting)
    {
        snow.gameObject.SetActive(setting);
    }

    public void setFog(bool setting)
    {
        fog.gameObject.SetActive(setting);
    }
}
