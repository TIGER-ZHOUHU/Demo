using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    //定义一个Transform类型的player
    [SerializeField ]private Transform player;

    //位置偏移（摄像机与人）
    private Vector3 offsetPosition;
    

    void Start()
    {
        //让摄像机面向角色位置
        transform.LookAt(player.position);
        //得到偏移量
        offsetPosition = transform.position - player.position;

    }

    // Update is called once per frame
    void Update()
    {
        //让摄像机的位置 = 人物行走的位置 + 与偏移量的相加
        transform.position = offsetPosition + player.position;
    }
}
