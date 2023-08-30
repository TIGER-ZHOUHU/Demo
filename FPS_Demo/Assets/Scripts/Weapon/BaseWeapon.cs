using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWeapon : MonoBehaviour
{
     public WeaponDataSO weaponData;
     public Transform shootPoint;


    /// <summary>
    /// 武器攻击功能
    /// </summary>
    public void Shoot()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray,out hit))
        {
            Vector3 targetPosition = hit.point;
            Vector3 position = shootPoint.position;
            Vector3 shootDirection = targetPosition - position;
            
            GameObject bullet = Instantiate(weaponData.bulletPrefab, position, Quaternion.LookRotation(shootDirection));
            
            //得到子弹刚体组件
            Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
            bulletRigidbody.AddForce(shootPoint.forward * 500f);//在射击方向上施加力量，控制子弹的速度
        }
    }

    /// <summary>
    /// 重新装子弹
    /// </summary>
    /*public void Reload()
    {
        weaponData.ammoCurrentCount = weaponData.ammoMaxCount;
    }*/
}
