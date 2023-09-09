using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerStateObserver
{
    void OnPlayerHealthChanged(int newHealth);
}
