using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Exit : GameElement
{
    [SerializeField] private UnityEvent OnWin;
    [SerializeField] private UnityEvent OnUnblock;
    [SerializeField] private UnityEvent OnKeysChanged;
    private bool IsFindAllKeys;

    public override void Init(GameState state)
    {
        base.Init(state);
        State.OnKeysCountChanged += KeysChanged;
    }

    private void KeysChanged()
    {
        OnKeysChanged.Invoke();
        IsFindAllKeys = State.KeysCount == GameState.MaxKey;
        if (IsFindAllKeys)
        {
            OnUnblock.Invoke();
        }
    }

    protected override void OnColisionWithPlayer()
    {
        if (IsFindAllKeys)
        {
            OnWin.Invoke();
        }
    }
}
