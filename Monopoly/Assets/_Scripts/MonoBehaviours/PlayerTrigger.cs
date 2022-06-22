using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public class PlayerTrigger : MonoBehaviour
{
    [HideInInspector] public UnityEvent<bool> PlayerOnTrigger = new UnityEvent<bool>();

    private void OnTriggerStay(Collider _collider)
    {
        if (_collider.CompareTag("Player"))
            PlayerOnTrigger?.Invoke(true);
    }

    private void OnTriggerExit(Collider _collider)
    {
        if (_collider.CompareTag("Player"))
            PlayerOnTrigger?.Invoke(false);
    }
}
