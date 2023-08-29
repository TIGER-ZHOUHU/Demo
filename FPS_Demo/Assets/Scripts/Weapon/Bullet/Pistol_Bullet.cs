using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol_Bullet : MonoBehaviour
{
    void Start () {
        Destroy(gameObject, 7f);  //7s后销毁自身
    }
}
