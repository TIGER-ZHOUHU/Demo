using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;


public class EnemyController : MonoBehaviour
{
    private Animator _animator;
    public EnemyDataSO _enemyData;
    private Transform target;
    private float _speed = 4f;

    private float stoppingDistance = 1.5f;//目标停止距离
    private bool isMoving = true; //是否正在移动

    private int enemyAtk = 1;
    private void Start()
    {
        _animator = GetComponent<Animator>();
        //让生成出来的敌人的当前生命值为最大生命值
        _enemyData.currentHealth = _enemyData.maxHealth;
        _enemyData.atk = enemyAtk;
        
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
                _enemyData.currentHealth -= weapon.weaponData.damage;
                _animator.SetTrigger("Hurt");
            }
        }
    }
    public void SetTarget(Transform targetTransform)
    {
        if (targetTransform)
        {
            target = targetTransform;
        }
    }
    private void MoveToPlayer()
    {
        if (isMoving)
        {
            if (!target)
            {
                target = transform;
            }
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
    

    
}
