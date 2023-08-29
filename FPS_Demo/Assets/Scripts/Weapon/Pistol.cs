using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Pistol : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform shootPoint;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray,out hit))
        {
            Vector3 targetPosition = hit.point;
            Vector3 shootDirection = targetPosition - shootPoint.position;
            
            GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.LookRotation(shootDirection));
            //得到子弹刚体组件
            Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
            bulletRigidbody.AddForce(shootPoint.forward * 500f);//在射击方向上施加力量，控制子弹的速度
        }

        //实例化子弹预制体

    }
}
