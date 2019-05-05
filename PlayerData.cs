using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerData")]
public class PlayerData : ScriptableObject
{
    [SerializeField]
    private string name;

    [SerializeField]
    private Sprite[] avatars;

    public string getName()
    {
        return name;
    }

    public Sprite[] getAvatars()
    {
        return avatars;
    }

}
