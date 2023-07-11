using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance { get; private set; }

    public static event Action OnToastHit;
    public static event Action OnGameOver;
    
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void RaiseOnToastHit()
    {
        OnToastHit?.Invoke();
    }

    public void RaiseOnGameOver()
    {
        OnGameOver?.Invoke();
    }
}
