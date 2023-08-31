using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Pistol : BaseWeapon
{
    //用来存储当前时间
    private float Timing = 2f;

    void Update()
    {
        Timing += Time.deltaTime;
        if (Input.GetMouseButtonDown(0))
        {
            PistolShoot();
        }
    }

    private void PistolShoot()
    {
        if (Timing >= weaponData.interval)
        {
            Shoot();
            Timing = 0;
        }
    }
    
}
