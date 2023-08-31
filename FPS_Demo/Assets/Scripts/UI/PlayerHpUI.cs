using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHpUI : MonoBehaviour
{
    [SerializeField]private Image hpImage;
    [SerializeField]private TextMeshProUGUI hpText;

    public void SetHpShowUI(PlayerDataSO playerData)
    {
        hpImage.fillAmount = (float)playerData.currentHealth / playerData.maxHealth;
        hpText.text = playerData.currentHealth + " / " + playerData.maxHealth;
    }
    

}
