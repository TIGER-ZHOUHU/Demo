using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Pistol : MonoBehaviour
{
    public static Pistol instance;
    
    public WeaponDataSO weaponData;
    private Camera mainCamera;
    private Transform shootPoint;
    [SerializeField] private AudioClip[] fire;

    private AudioSource pistolShoot;

    private void Awake()
    {
        instance = this;
        mainCamera = Camera.main;
        shootPoint = GameObject.FindWithTag("BulletPosition").transform;
        pistolShoot = GetComponent<AudioSource>();
    }

    private void Start()
    {
        pistolShoot.clip = fire[0];
    }

    public void SwitchClip(ShootState shootState)
    {
        int index = (int)shootState;
        Debug.Log(index);
        pistolShoot.clip = fire[index];
    }
    public void Shoot(bool isCool, Transform player)
    {
        if (isCool)
        {
            return;
        }
        if (mainCamera != null)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray,out hit))
            {
                Vector3 worldMousePostion = hit.point;
                Vector3 shootDirection = worldMousePostion - player.position;
                //Debug.Log(worldMousePostion);
                
                GameObject bullet = ObjectPool.instance.GetBullet();
                bullet.transform.position = shootPoint.position;
                bullet.transform.rotation = Quaternion.LookRotation(shootDirection);
                //Debug.Log(bullet.transform.rotation);
                //Vector3 movement = new Vector3(shootDirection.x, 0, shootDirection.z) * (10 * Time.deltaTime);
               // bullet.transform.position += movement;
                //得到子弹刚体组件
                Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
                bulletRigidbody.useGravity = false;
                bulletRigidbody.velocity = 10 * bullet.transform.forward;
                // bulletRigidbody.AddForce(shootPoint.forward * 300f);//在射击方向上施加力量，控制子弹的速度
            }
        }
        pistolShoot.Play();
    }
    
}
