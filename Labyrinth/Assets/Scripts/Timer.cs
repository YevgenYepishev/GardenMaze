using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Time;
    [SerializeField] private TextMeshProUGUI[] FinalTime;
    private DateTime Value;
    private IEnumerator Coroutine;
    private string Text => Value.ToString("mm:ss");

    public void Stop()
    {
        StopCoroutine(Coroutine);
        foreach (TextMeshProUGUI item in FinalTime)
        {
            item.text = Text;
        }
    }

    private void Start()
    {
        Coroutine = TimeCount();
        StartCoroutine(Coroutine);
    }
    
    private IEnumerator TimeCount()
    {
        while (true)
        {
            Value = Value.AddSeconds(1);
            Time.text = Text;
            yield return new WaitForSeconds(1);
        }
    }
}
