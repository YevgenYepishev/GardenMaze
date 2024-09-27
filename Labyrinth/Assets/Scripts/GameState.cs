using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState
{
    public const int MaxKey = 2;
    public int KeysCount = 0;
    public event Action OnKeysCountChanged;
    public Action OnTrap;

    public void IncrementKey()
    {
        KeysCount++;
        OnKeysCountChanged.Invoke();
    }
}
