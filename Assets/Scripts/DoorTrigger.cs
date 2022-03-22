using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public delegate void OnDoorTriggerEnterDelegate();
    public OnDoorTriggerEnterDelegate OnDoorTriggerEnter;

    public delegate void OnDoorTriggerExitDelegate();
    public OnDoorTriggerExitDelegate OnDoorTriggerExit;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            OnDoorTriggerEnter?.Invoke();
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            OnDoorTriggerExit?.Invoke();
        }
    }
}
