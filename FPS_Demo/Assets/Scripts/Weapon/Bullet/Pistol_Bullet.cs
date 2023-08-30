using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol_Bullet : MonoBehaviour
{
    void Start () {
        Destroy(gameObject, 5f);  //5s后销毁自身
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }
}
