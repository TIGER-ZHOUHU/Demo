using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerShootStateUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI shootStateUI;

    public void SetPlayerShootStateUI(ShootState state)
    {
        shootStateUI.text = Enum.GetName(typeof(ShootState),state);
    }
}
