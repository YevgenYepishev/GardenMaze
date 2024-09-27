using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BoxMazeComponent : MonoBehaviour
{
    [SerializeField] private MazeProperty MazeProperty;
    [SerializeField] private UnityEvent OnMazeCreated;

    public void Init(Vector2Int mazeSize)
    {
        MazeProperty.MazeSize = mazeSize;
        GenerateMaze();
    }

    [ContextMenu("Generate maze")]
    private void GenerateMaze()
    {
        Rooms rooms = new Rooms(MazeProperty);
        rooms.CreateWalls();
        GenerateMaze(rooms);
            
        // delay for the next frame
        Invoke(nameof(MazeCreated), 0f);
    }

    private void MazeCreated()
    {
        OnMazeCreated.Invoke();
    }

    private void GenerateMaze(Rooms rooms)
    {
        Vector2Int curRoomPos = Vector2Int.zero;
        List<Vector2Int> visitedRooms = new List<Vector2Int>();
        visitedRooms.Add(curRoomPos);
            
        int roomCount = MazeProperty.MazeSize.x * MazeProperty.MazeSize.y;
        for (int i = 1; i < roomCount;)
        {
            Room tempRoom = rooms[curRoomPos];
            Vector2Int position = tempRoom.NextRoom();

            if (!visitedRooms.Contains(position))
            {
                if (rooms[position].PreviousRoom == null)
                    rooms[position].PreviousRoom = tempRoom;

                visitedRooms.Add(curRoomPos);
                i++;
            }

            curRoomPos = position;
        }
    }
}