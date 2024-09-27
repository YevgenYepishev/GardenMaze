using UnityEngine;

public class Rooms
{
    private readonly Room[,] RoomsLinkedList;
    private readonly Vector2Int Count;

    public Rooms(MazeProperty mazeProperty)
    {
        Count = mazeProperty.MazeSize;
        RoomsLinkedList = new Room[Count.x, Count.y];
        for (int x = 0; x < Count.x; x++)
        {
            for (int y = 0; y < Count.y; y++)
            {
                RoomsLinkedList[x, y] = new Room(mazeProperty);
            }
        }
    }

    public Room this[Vector2Int position]
    {
        get => RoomsLinkedList[position.x, position.y];
        private set => RoomsLinkedList[position.x, position.y] = value;
    }

    public void CreateWalls()
    {
        // split into two loops with constructor to avoid null reference exception
        for (int x = 0; x < Count.x; x++)
        {
            for (int y = 0; y < Count.y; y++)
            {
                Vector2Int position = new Vector2Int(x, y);
                CreateWalls(position);
            }
        }
    }

    private void CreateWalls(Vector2Int position)
    {
        this[position].IsCreated = true;

        for (int i = 0; i < (int)Direction.Length; i++)
        {
            if (!CheckRoomsEdge(i, position))
                continue;

            Vector2Int neighborsPosition = position + Room.GetWallDirection(i);
            this[position].Neighbors[i] = this[neighborsPosition];
        }

        this[position].PositionIndexes = position;
        this[position].CreateWalls();
    }

    private bool CheckRoomsEdge(int index, Vector2Int pos)
    {
        switch ((Direction)index)
        {
            case Direction.Left:
                return pos.x > 0;
            case Direction.Right:
                return pos.x < Count.x - 1;
            case Direction.Back:
                return pos.y > 0;
            case Direction.Forward:
                return pos.y < Count.y - 1;
            default:
                return false;
        }
    }
}