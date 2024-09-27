using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapsHolder : MonoBehaviour
{
    [SerializeField] private Trap[] Traps;

    public void Init(GameState state)
    {
        foreach (Trap trap in Traps)
        {
            trap.Init(state);
        }
    }
}
