using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Game/Player Data")]
public class PlayerDataSO : ScriptableObject
{
    public int currentHealth;
    public int maxHealth = 5;

    public int attack;
    public bool isDead = false;
    
    public void Init()
    {
        maxHealth = 5;
        currentHealth = maxHealth;
        attack = 1;
        isDead = false;
    }

    public string HPShow()
    {
        return currentHealth.ToString() + " / " + maxHealth.ToString();
    }
}
