using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "EnemyData", menuName = "Game/Enemy Data")]
public class EnemyDataSO : ScriptableObject
{
    public int currentHealth;
    public int maxHealth = 4;
    public int Id;
    public int atk = 1;

    public bool IsDead;
    
    public void SetEnemyData()
    {
        maxHealth = 4;
        currentHealth = maxHealth;
        atk = 1;
    }
    
}
