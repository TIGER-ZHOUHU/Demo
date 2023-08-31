using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;
    
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private EnemyDataCollection enemyDataCollection;
    [SerializeField] private TextMeshProUGUI scoreText;

    [Header("Enemy Count")]
    private int maxEnemyCount = 10;
    public int currentEnemyCount;

    [Header("SpawnPosition")] 
    private float spawnDistanceMax = 20f;
    private float spawnDistanceMin = 8f;

    [Header("Timer")] 
    private float spawnTimeMax = 0.5f;
    private float currentTime;

    private int playerScore = 0;

    
    private void Awake()
    {
        Instance = this;
        enemyDataCollection.SetEnemyData();
    }

    private void Update()
    {
        currentTime += Time.deltaTime;
        if (currentEnemyCount < maxEnemyCount && currentTime > spawnTimeMax && playerTransform)
        {
            SpawnerEnemy();
            currentTime = 0f;
        }
    }

    #region 生成敌人

    private void SpawnerEnemy()
    {
        //随机选择一个敌人数据
        
        //int randomIndex = Random.Range(0, enemyDataCollection.enemyDataArray.Length);
        EnemyDataSO enemyData = enemyDataCollection.GetOneEnemyData();

        Vector3 spawmPosition = GetRandomSpawnPosition();
        if (IsPositionValid(spawmPosition))
        {
            //在场景中生成敌人对象
            GameObject newEnemy = Instantiate(enemyPrefab, spawmPosition, Quaternion.identity);
            //获取敌人的控制脚本，并将随机选择的敌人数据分配给它
            EnemyController enemyController = newEnemy.GetComponent<EnemyController>();
            enemyController._enemyData = enemyData;
            enemyController.SetTarget(playerTransform);
        }
        currentEnemyCount++;
    }

    private Vector3 GetRandomSpawnPosition()
    {
        float spawnDistance = Random.Range(spawnDistanceMin, spawnDistanceMax);
        Vector2 randomCircle = Random.insideUnitCircle.normalized * spawnDistance;
        Vector3 randomPosition = new Vector3(randomCircle.x, 0f, randomCircle.y);

        return playerTransform.position + randomPosition;
    }

    private bool IsPositionValid(Vector3 position)
    {
        Collider[] colliders = Physics.OverlapSphere(position, enemyPrefab.GetComponent<Collider>().bounds.extents.x);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                return false;
            }
        }
        return true;
    }

    #endregion

    #region 敌人死亡

    public void EnemyDie(GameObject obj)
    {
        Destroy(obj);
        //TODO:不同敌人加不同的分数
        playerScore++;
        scoreText.text = playerScore.ToString();
    }

    #endregion

}
