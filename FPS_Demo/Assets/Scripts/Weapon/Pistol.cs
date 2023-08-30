using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Pistol : BaseWeapon
{
    //用来存储当前时间
    private float Timing = 2f;
    // Update is called once per frame
    void Update()
    {
        Timing += Time.deltaTime;
        if (Input.GetMouseButtonDown(0))
        {
            PistolShoot();
        }
    }

    public void PistolShoot()
    {
        if (Timing >= weaponData.interval)
        {
            Shoot();
            Timing = 0;
        }
    }
    
}
