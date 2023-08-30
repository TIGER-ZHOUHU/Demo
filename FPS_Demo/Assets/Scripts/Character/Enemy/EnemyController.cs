using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;


public class EnemyController : MonoBehaviour
{
    public EnemyDataSO _enemyData;
    private Transform target;
    private float _rotate = 10f;
    private float _speed = 4f;
    

    private void Start()
    {
        _enemyData.currentHealth = 4;
    }

    void Update()
    {
        MoveToPlayer();
        if (_enemyData.currentHealth <=0)
        {
            Destroy(gameObject);
            EnemyManager.Instance.currentEnemyCount--;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            _enemyData.currentHealth--;
        }
    }

    
    private void MoveToPlayer()
    {
        Vector3 direction = target.transform.position - transform.position;

        direction.Normalize();
        transform.Translate( direction * (_speed * Time.deltaTime));
    }
    
    public void SetTarget(Transform targetTransform)
    {
        target = targetTransform;
    }
    
}
