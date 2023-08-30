using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyDataCollection",menuName = "Game/Enemy Data Collection")]
public class EnemyDataCollection : ScriptableObject
{
    public EnemyDataSO[] enemyDataArray;
}
