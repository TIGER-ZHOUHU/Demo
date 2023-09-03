using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyDataCollection",menuName = "Game/Enemy Data Collection")]
public class EnemyDataCollection : ScriptableObject
{
    public EnemyDataSO[] enemyDataArray ;
    private EnemyDataSO enemyData;
    //private Dictionary<int, EnemyDataSO> enemyDatas = new Dictionary<int, EnemyDataSO>();
    
    /*public void SetEnemyData()
    {
        //Add enemyDataArray To enemyDatas
        foreach (EnemyDataSO data in enemyDataArray)
        {
            //不包含，意味着当前拥有enemyData的敌人死亡
            if (!enemyDatas.ContainsValue(data))
            {
                //TODO：增加了升级机制，每次增加10个，这个的方法不得行了，可能这段都要修改，之前想复杂了
                enemyDatas.Add(data.Id,data);
                data.currentHealth = data.maxHealth;
                data.IsDead = true;
            }
        }
    }

    public EnemyDataSO GetOneEnemyData()
    {
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
    }*/

    public void SetEnemyDataUseOneData()
    {
        enemyData = enemyDataArray[0];
    }

    public EnemyDataSO GetFirstEnemyData()
    {
        return enemyDataArray[0];
    }
    
}
