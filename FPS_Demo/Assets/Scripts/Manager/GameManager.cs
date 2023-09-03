using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private PlayerController player;

    private bool isStop = false;
    private int levelEnemyCount = 10;
    private int level = 1;

    private bool isTreble = false;
    private bool isContinous = false;
    

    private void Awake()
    {
        instance = this;
        Time.timeScale = 0;
    }

    private void Update()
    {
        if ( ReferenceEquals(player,null) || Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            StopGame();
        }

        if (EnemyManager.Instance.GetPlayerScore() > levelEnemyCount)
        {
            StartCoroutine(LevelUP());
        }
    }

    private IEnumerator LevelUP()
    {

        levelEnemyCount *= 2;
        yield return new WaitForSeconds(0.1f);
        
        EnemyManager.Instance.SetEnemyMaxCount(10);
        EnemyManager.Instance.SetEnemyAtk(1*level,5*level);
        PlayerController.instance.SetPlayerLevel(3*level, 3*level);
        level++;
        if (level >= 2)
        {
            isTreble = true;
        }

        if (level >= 3)
        {
            isContinous = true;
        }
    }
    private void StopGame()
    {
        if (isStop)
        {
            Time.timeScale = 0;
            isStop = !isStop;
        }
        else
        {
            Time.timeScale = 1;
            isStop = !isStop;
        }
    }

    public bool GetIsTreble()
    {
        return isTreble;
    }

    public bool GetIsContinous()
    {
        return isContinous;
    }
}
