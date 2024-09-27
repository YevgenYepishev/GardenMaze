using UnityEngine;

public abstract class GameElement : MonoBehaviour
{
    protected GameState State;

    public virtual void Init(GameState state)
    {
        State = state;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            OnColisionWithPlayer();
    }

    protected abstract void OnColisionWithPlayer();
}
