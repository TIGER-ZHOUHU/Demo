using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol_Bullet : MonoBehaviour
{
    public WeaponDataSO weaponData;

    [SerializeField] private AudioClip[] _audioAtack;
    private AudioSource _audioSource;
    private void OnEnable()
    {
        StartCoroutine(DelayRecycle());
    }
    
    private IEnumerator DelayRecycle()
    {
        yield return new WaitForSeconds(3f);
        ObjectPool.instance.ReturnBullet(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            ObjectPool.instance.ReturnBullet(gameObject);
        }
    }
}
