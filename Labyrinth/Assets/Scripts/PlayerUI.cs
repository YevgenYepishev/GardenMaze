using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI KeyCount;
    [SerializeField] private UnityEvent OnDie;
    [SerializeField] private UnityEvent OnPause;
    [SerializeField] private UnityEvent OnFindAllKeys;
    private GameState State;

    public void Init(GameState state)
    {
        State = state;
        State.OnKeysCountChanged += UpdateKeyCount;
        State.OnTrap += OnDie.Invoke;
        UpdateKeyCount();
    }

    public void Restart()
    {
        SceneManager.LoadScene("Maze");
    }

    public void Exit()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnPause.Invoke();
        }
    }

    private void UpdateKeyCount()
    {
        if (KeyCount != null)
        {
            KeyCount.text = State.KeysCount + "/" + GameState.MaxKey;
        }

        if (State.KeysCount == GameState.MaxKey)
        {
            OnFindAllKeys.Invoke();
        }
    }
}
