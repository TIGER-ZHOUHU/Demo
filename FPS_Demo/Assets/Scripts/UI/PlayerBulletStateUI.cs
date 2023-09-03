using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
public class PlayerBulletStateUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI BulletStateUI;

    public void SetPlayerbulletStateUI(BulletState state)
    {
        BulletStateUI.text = Enum.GetName(typeof(BulletState),state);
    }
}
