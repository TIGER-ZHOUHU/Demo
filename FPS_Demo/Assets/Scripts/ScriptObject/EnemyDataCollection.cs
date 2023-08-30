using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyDataCollection",menuName = "Game/Enemy Data Collection")]
public class EnemyDataCollection : ScriptableObject
{
    public EnemyDataSO[] enemyDataArray;

    private Dictionary<int, EnemyDataSO> enemyDatas = new Dictionary<int, EnemyDataSO>();

    public void SetEnemyData()
    {
        //Add enemyDataArray To enemyDatas
        foreach (EnemyDataSO data in enemyDataArray)
        {
            if (!enemyDatas.ContainsValue(data))
            {
                enemyDatas.Add(data.Id,data);
                data.currentHealth = data.maxHealth;
                data.IsDead = true;
            }
        }
    }

    public EnemyDataSO GetOneEnemyData()
    {
        EnemyDataSO enemyData = null;
        foreach (EnemyDataSO data in enemyDataArray)
        {
            if (data.IsDead)
            {
                enemyData = enemyDatas[data.Id];
                data.IsDead = false;
                return enemyData;
            }
        }

        return null;
    }
    
}
