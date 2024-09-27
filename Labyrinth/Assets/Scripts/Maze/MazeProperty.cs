using System;
using UnityEngine;

[Serializable]
public class MazeProperty
{
    public float WallLength = 1f;
    public WallPrefabs Wall;
    public Transform WallHolder;
    public Vector2Int MazeSize;
        
    public Vector3 GetVectorPoint(Vector2Int source)
    {
        return new Vector3(GetPartOfVector(source.x), 0, GetPartOfVector(source.y));
    }

    private float GetPartOfVector(float point)
    {
        float halfLength = WallLength * 0.5f;
        return point * WallLength + halfLength;
    }
}