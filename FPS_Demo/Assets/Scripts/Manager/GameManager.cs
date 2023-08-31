using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private PlayerController player; 

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (player == null)
        {
            Application.Quit();
        }
    }
}
