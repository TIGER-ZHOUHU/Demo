using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
public enum  ShootState
{
    Single,
    Treble,
    Continuous
}

public enum BulletState
{
    Normal,
    Explode,
    Laser
    
}
public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    
    public event Action<int> OnBulletStateChange;
    
    [Header("GameObject")]
    [SerializeField] private GameObject player;

    [SerializeField] private PlayerDataSO playerData;

    [SerializeField] private PlayerHpUI playerHp;
    [SerializeField] private PlayerShootStateUI playerShootStateUI;
    [SerializeField] private PlayerBulletStateUI playerBulletStateUI;
    [SerializeField] private AudioClip playerHurt;

    private Animator _animator;
    private Camera _mainCamera;
    private AudioSource _audioSource;
    
    [Header("Transform")]
    private float m_speed = 8f;
    private float m_rotate = 10f;

    private bool isCool = false;
    private bool isInvincible = false;

    //射击状态
    private ShootState _shootState;
    private int _shootStateCount = 1;

    private BulletState _bulletState;
    private int _bulletStateCount = 3;

    private void Awake()
    {
        instance = this;
        playerData.Init();
        _mainCamera = Camera.main;
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        
        _audioSource.clip = playerHurt;
        _shootState = ShootState.Single;
        _bulletState = BulletState.Normal;
        
        playerHp.SetHpShowUI(playerData);
        playerShootStateUI.SetPlayerShootStateUI(_shootState);
        playerBulletStateUI.SetPlayerbulletStateUI(_bulletState);
    }

    void Update()
    {
        //键盘控制移动
        PlayerMove_KeyTransform();
        PlayerState();
        if (Input.GetMouseButtonDown(1))
        {
            SwitchToNextShootState();
        }
        
        if (Input.GetMouseButtonDown(0) && !isCool)
        {
            PlayerShoot(_shootState);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            SwitchToNextBulletState();
        }
    }

    private void SwitchToNextBulletState()
    {
        int nextStateIndex = ((int)_bulletState + 1) % _bulletStateCount;
        _bulletState = (BulletState)nextStateIndex;
        playerBulletStateUI.SetPlayerbulletStateUI(_bulletState);
        OnBulletStateChange?.Invoke(nextStateIndex);
    }
    private void SwitchToNextShootState()
    {
        int nextStateIndex;
        if (GameManager.instance.GetIsTreble())
        {
            _shootStateCount = 2;
        }
        if (GameManager.instance.GetIsContinous())
        {
            _shootStateCount = 3;
        }
        nextStateIndex = ((int)_shootState + 1) % _shootStateCount;

        _shootState = (ShootState)nextStateIndex;
        Pistol.instance.SwitchClip(nextStateIndex);
        playerShootStateUI.SetPlayerShootStateUI(_shootState);
        
    }
    private void PlayerShoot(ShootState state)
    {
        switch (state)
        {
            case ShootState.Single:
                StartCoroutine(SingleShoot());
                break;
            case ShootState.Treble:
                StartCoroutine(TrebleShoot());
                break;
            case ShootState.Continuous:
                StartCoroutine(CoutinuousShoot());
                break;
        }
    }

    private IEnumerator CoutinuousShoot()
    {
        while (_shootState == ShootState.Continuous)
        {
            Pistol.instance.Shoot(isCool,player.transform);
            isCool = true;
            yield return new WaitForSeconds(Pistol.instance.weaponData.interval/2);
            isCool = false;
        }
    }

    private IEnumerator TrebleShoot()
    {
        for (int i = 0; i < 3; i++)
        {
            Pistol.instance.Shoot(isCool,player.transform);
            isCool = true;
            yield return new WaitForSeconds(Pistol.instance.weaponData.interval/2);
            isCool = false;
        }
    }

    private IEnumerator SingleShoot()
    {
        Pistol.instance.Shoot(isCool,player.transform);
        isCool = true;
        yield return new WaitForSeconds(Pistol.instance.weaponData.interval);
        isCool = false;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy") && !isInvincible)
        {
            EnemyController enemy = collision.collider.GetComponent<EnemyController>();
            Transform enemyTransform = collision.collider.GetComponent<Transform>();
            if (enemy)
            {
                playerData.currentHealth -= enemy.GetEnemyAtk();
                playerHp.SetHpShowUI(playerData);
                _animator.SetTrigger("Hurt");
                _audioSource.Play();
                
                Vector3 dir = enemyTransform.position - transform.position;
                dir.Normalize();
                transform.position = Vector3.Lerp(transform.position,transform.position - new Vector3(dir.x,0,dir.z),0.1f);
                
                isInvincible = true;
            }
        }

        StartCoroutine(AttackedCool());
    }

    private IEnumerator AttackedCool()
    {
        yield return new WaitForSeconds(1f);
        isInvincible = false;
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
        if (_mainCamera != null)
        {
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray,out hit) )
            {
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
                    //transform.rotation = Quaternion.LookRotation(direction);
                }
            }
        }
        // 处理键盘输入
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * (m_speed * Time.deltaTime);
        transform.position += movement;
    }

    public int PlayerAttack()
    {
        return playerData.attack;
    }

    public void SetPlayerLevel(int atkLevel,int maxHealthLevel)
    {
        playerData.attack += atkLevel;
        playerData.maxHealth += maxHealthLevel;
        playerData.currentHealth = playerData.maxHealth;
        playerHp.SetHpShowUI(playerData);
    }
    
}
