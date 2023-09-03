using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol_Bullet : MonoBehaviour
{
    public WeaponDataSO weaponData;
    
    [SerializeField] private AudioClip[] _audioAtack;
    private AudioSource _audioSource;
    public bool isDamaged = false;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        _audioSource.clip = _audioAtack[0];
        PlayerController.instance.OnBulletStateChange += InstanceOnOnBulletStateChange;
    }

    private void InstanceOnOnBulletStateChange(int bulletStateIndex)
    {
        SwitchClip(bulletStateIndex);
    }
    private void SwitchClip(int bulletStateIndex)
    {
        _audioSource.clip = _audioAtack[bulletStateIndex];
    }
    private void OnEnable()
    {
        isDamaged = false;
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
            _audioSource.Play();
            StartCoroutine(WaitForAudioEnd());
        }
    }

    private IEnumerator WaitForAudioEnd()
    {
        yield return new WaitForSeconds(_audioSource.clip.length/35);
        
        ObjectPool.instance.ReturnBullet(gameObject);
    }

}
