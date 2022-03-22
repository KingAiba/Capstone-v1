using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValidatorScript : MonoBehaviour
{
    public bool isCollidingWithRoom = false;

    private void FixedUpdate()
    {
        isCollidingWithRoom = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Validator"))
        {
            isCollidingWithRoom = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Validator"))
        {
            isCollidingWithRoom = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Validator"))
        {
            isCollidingWithRoom = false;
        }
    }


}
