﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField]
    private PlayerData playerData;

    public PlayerData getPlayerData() => playerData;
}
