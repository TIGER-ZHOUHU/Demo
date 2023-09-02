using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance;
    public GameObject bulletPrefab;
    private int poolSize = 30;

    [SerializeField]
    private List<GameObject> bulletPool;

    private void Awake()
    {
        instance = this;
        //初始化对象池
        for (int i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.transform.position = Vector3.zero;
            bullet.SetActive(false);
            bulletPool.Add(bullet);
        }
    }
    
    public GameObject GetBullet()
    {
        //从对象池中获取可用的子弹对象
        for (int i = 0; i < bulletPool.Count; i++)
        {
            if (!bulletPool[i].activeInHierarchy)
            {
                bulletPool[i].SetActive(true);
                return bulletPool[i];
            }
        }
        //如果对象池中没有可用的子弹对象，则创建一个新的子弹对象并添加到对象池中
        GameObject newBullet = Instantiate(bulletPrefab);
        bulletPool.Add(newBullet);
        return newBullet;
    }

    public void ReturnBullet(GameObject bullet)
    {
        bullet.transform.position = Vector3.zero;
        bullet.transform.rotation = Quaternion.identity;
        // 将子弹对象回收到对象池中
        bullet.SetActive(false);
    }
    
}
