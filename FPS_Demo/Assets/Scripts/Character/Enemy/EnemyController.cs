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
    private float _speed = 4f;

    private float stoppingDistance = 1.5f;//目标停止距离
    private bool isMoving = true; //是否正在移动
    

    private void Start()
    {
        //让生成出来的敌人的当前生命值为最大生命值
        _enemyData.currentHealth = _enemyData.maxHealth;
    }

    void Update()
    {
        MoveToPlayer();
        if (_enemyData.currentHealth <=0)
        {
            _enemyData.IsDead = true;
            EnemyManager.Instance.EnemyDie(gameObject);
            EnemyManager.Instance.currentEnemyCount--;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            BaseWeapon weapon = other.GetComponent<BaseWeapon>();
            if (weapon != null)
            {
                int damage = weapon.weaponData.damage;
                _enemyData.currentHealth -= damage;
            }
        }
    }

    
    private void MoveToPlayer()
    {
        if (isMoving)
        {
            float distanceToPlayer = Vector3.Distance(target.position, transform.position);
            if (distanceToPlayer < stoppingDistance)
            {
                isMoving = false;
                //TODO:敌人攻击
            }
            else
            {
                Vector3 direction = target.transform.position - transform.position;

                direction.Normalize();
                transform.Translate( direction * (_speed * Time.deltaTime));
            }
        }
    }
    
    public void SetTarget(Transform targetTransform)
    {
        target = targetTransform;
    }
    
}
