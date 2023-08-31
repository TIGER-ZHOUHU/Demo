using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    
    [Header("GameObject")]
    [SerializeField] private GameObject player;

    [SerializeField] private PlayerDataSO playerData;

    [SerializeField] private PlayerHpUI playerHp;
    private Animator _animator;

    [Header("Transform")]
    private float m_speed = 8f;
    private float m_rotate = 10f;

    private void Awake()
    {
        playerData.Init();
        instance = this;
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        //键盘控制移动
        PlayerMove_KeyTransform();
        PlayerState();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            EnemyController enemy = collision.collider.GetComponent<EnemyController>();
            Transform enemyTransform = collision.collider.GetComponent<Transform>();
            if (enemy)
            {
                playerData.currentHealth -= enemy._enemyData.atk;
                playerHp.SetHpShowUI(playerData);
                _animator.SetTrigger("Hurt");
                Vector3 dir = enemyTransform.position - transform.position;
                dir.Normalize();

                transform.position = Vector3.Lerp(transform.position,transform.position - new Vector3(dir.x,0,dir.z),0.1f);
            }
        }
    }
    
    private void PlayerState()
    {
        if (playerData.currentHealth < 0)
        {
            playerData.isDead = true;
            Destroy(gameObject);
        }
    }
    
    //通过Transform组件 键盘控制移动
    private void PlayerMove_KeyTransform()
    {
        //获取鼠标在世界空间中的位置
        if (Camera.main != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Physics.Raycast(ray,out hit); 
        
            Vector3 worldMousePostion = hit.point;
            //计算人物应该面向的方向
            Vector3 direction = worldMousePostion - player.transform.position;
            //确保人物只在水平方向上旋转
            direction.y = 0;
            if (direction != Vector3.zero)
            {
                //计算人物应该旋转的目标方向
                Quaternion targetRotation = Quaternion.LookRotation(direction);

                //平滑插值使人物旋转到目标方向
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, m_rotate * Time.deltaTime);
            }
        }
        // 处理键盘输入
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * (m_speed * Time.deltaTime);
        transform.position += movement;
    }


}
