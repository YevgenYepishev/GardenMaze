using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    public static Vector2Int MazeSize;
    [SerializeField] private GameElementsSpawner ElementSpawner;
    [SerializeField] private BoxMazeComponent MazeComponent;
    [SerializeField] private PlayerUI Player;
    private GameState State;

    private void Awake()
    {
        State = new GameState();
        ElementSpawner.Init(MazeSize, State);
        MazeComponent.Init(MazeSize);
        Player.Init(State);
    }
}

