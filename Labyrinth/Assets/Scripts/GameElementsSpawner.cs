using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameElementsSpawner : MonoBehaviour
{
    [SerializeField] private TrapsHolder[] TrapPrefabs;
    [SerializeField] private Key KeyPrefab;
    [SerializeField] private Exit Exit;
    [SerializeField] private float RoomSize;

    public void Init(Vector2Int mazeSize, GameState state)
    {
        CreateKeys(mazeSize, state);
        CreateTraps(mazeSize, state);
        Exit.Init(state);
        Exit.transform.localPosition = new Vector3((mazeSize.x - 1) * RoomSize, 0, (mazeSize.y - 1) * RoomSize);
    }

    private void CreateKeys(Vector2Int mazeSize, GameState state)
    {
        for (int i = 0; i < GameState.MaxKey; i++)
        {
            Key key = Instantiate<Key>(KeyPrefab, transform);
            key.Init(state);
            Vector3 direction = i == 0 ? Vector3.right : Vector3.forward;
            float linesCount = i == 0 ? mazeSize.x : mazeSize.y;
            key.transform.localPosition = direction * (linesCount - 1) * RoomSize;
        }
    }

    private void CreateTraps(Vector2Int mazeSize, GameState state)
    {
        int trapsCount = mazeSize.x > mazeSize.y ? mazeSize.x : mazeSize.y; //count trap equal biggest side of maze
        for (int i = 0; i < trapsCount; i++)
        {
            int index = Random.Range(0, TrapPrefabs.Length);
            TrapsHolder trapPrefab = TrapPrefabs[index];
            TrapsHolder trap = Instantiate<TrapsHolder>(trapPrefab, transform);
            trap.Init(state);
            int xRoom = Random.Range(0, mazeSize.x);
            int yRoom = Random.Range(0, mazeSize.y);
            trap.transform.localPosition = new Vector3(xRoom, 0, yRoom) * RoomSize;
        }
    }
}
