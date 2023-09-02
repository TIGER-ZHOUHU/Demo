using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new WeaponData",menuName = "Game/WeaponData")]
public class WeaponDataSO : ScriptableObject
{
    //无限子弹
    
    [Header("Basic Attribute")]
    //武器名字
    public string weaponName;
    //武器伤害
    public int damage;
    //是否是远程武器
    public bool isLongRange;
    //武器攻击间隔
    public float interval = 1f;
    //子弹预制体
    //public GameObject bulletPrefab;
    //子弹出发位置


    //TODO:还没想好怎么用range
    //public float range; 
    //子弹数量
    /*public int ammoCurrentCount;
    public int ammoMaxCount;*/


}
