using System;
using UnityEngine;

[Serializable]
public class WallPrefabs
{
    public GameObject Wall;
    public GameObject WallLeft;

    public GameObject GetPrefab(Direction direction)
    {
        switch (direction)
        {
            case Direction.Left:
            case Direction.Right:
                return WallLeft;
            default:
                return Wall;
        }
    }
}
