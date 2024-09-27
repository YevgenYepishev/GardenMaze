using System.Collections.Generic;
using UnityEngine;

public class Room
{
    public bool IsCreated;
    public Room PreviousRoom;
    public Vector2Int PositionIndexes;

    public readonly Room[] Neighbors = new Room[(int)Direction.Length];

    private readonly MazeProperty MazeProperty;
    private readonly GameObject[] Walls = new GameObject[(int)Direction.Length];

    public Room(MazeProperty mazeProperty)
    {
        MazeProperty = mazeProperty;
    }

    public void CreateWalls()
    {
        Vector3 point = MazeProperty.GetVectorPoint(PositionIndexes);

        for (int i = 0; i < (int)Direction.Length; i++)
        {
            if (Neighbors[i] != null && Neighbors[i].IsCreated)
            {
                // wall already created on previous room
                Walls[i] = Neighbors[i].Walls[GetInvertWallIndex(i)];
                continue;
            }

            GameObject prefab = MazeProperty.Wall.GetPrefab((Direction)i);
            GameObject wall = Object.Instantiate(prefab, MazeProperty.WallHolder, false);
            wall.transform.localPosition = GetWallPosition(point, i);

            Walls[i] = wall;
        }
    }

    public Vector2Int NextRoom()
    {
        List<int> freeIndex = GetNeighborRooms();
        
        // case when all neighbors are already visited
        if (freeIndex.Count <= 0)
            return PreviousRoom.PositionIndexes;

        int dirIndex = Random.Range(0, freeIndex.Count);
        Object.Destroy(Walls[freeIndex[dirIndex]]);

        return Neighbors[freeIndex[dirIndex]].PositionIndexes;
    }

    private List<int> GetNeighborRooms()
    {
        List<int> result = new List<int>();

        for (int i = 0; i < Neighbors.Length; i++)
        {
            Room room = Neighbors[i];
            if (room != null && room.PreviousRoom == null && !room.PositionIndexes.Equals(Vector2Int.zero))
            {
                result.Add(i);
            }
        }

        return result;
    }

    private Vector3 GetWallPosition(Vector3 point, int directionValue)
    {
        Vector2Int direction = GetWallDirection(directionValue);
        Vector3 wallDirection = new Vector3(direction.x, 0, direction.y);
        float halfLength = MazeProperty.WallLength * 0.5f;
        return point + wallDirection * halfLength;
    }

    public static Vector2Int GetWallDirection(int index)
    {
        switch ((Direction)index)
        {
            case Direction.Left:
                return Vector2Int.left;
            case Direction.Right:
                return Vector2Int.right;
            case Direction.Back:
                return Vector2Int.down;
            default:
                return Vector2Int.up;
        }
    }

    private static int GetInvertWallIndex(int index)
    {
        if (index == (int)Direction.Left)
            return (int)Direction.Right;

        if (index % 2 != 0)
            return index - 1;

        return index != (int)Direction.Length ? index + 1 : (int)Direction.Length;
    }
}