using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    public static T Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = (T)this;
        }
        else
        {
            if (Instance != this)
            {
                Destroy(this.gameObject);
                return;
            }
        }

        Initialize();
    }

    protected abstract void Initialize();
}
