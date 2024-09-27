using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : GameElement
{
    [SerializeField] private Animator spikeTrapAnim;

    private IEnumerator OpenCloseTrap()
    {
        spikeTrapAnim.SetTrigger("open");
        yield return new WaitForSeconds(2);

        spikeTrapAnim.SetTrigger("close");
    }

    protected override void OnColisionWithPlayer()
    {
        StartCoroutine(OpenCloseTrap());
        State.OnTrap.Invoke();
    }
}
