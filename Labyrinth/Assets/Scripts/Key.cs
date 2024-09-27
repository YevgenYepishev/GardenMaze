using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class Key : GameElement
{
    protected override void OnColisionWithPlayer()
    {
        Destroy(gameObject);
        State.IncrementKey();
    }
}
