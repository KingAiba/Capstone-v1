using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHairTarget : MonoBehaviour
{
    Camera mainCamera;
    Ray ray;
    RaycastHit hitInfo;

    public GameObject player;
    public float maxRange = 1000.0f;
   
    void Start()
    {
        mainCamera = Camera.main;
    }


    void Update()
    {
        ray.origin = mainCamera.transform.position;
        ray.direction = mainCamera.transform.forward;

        if(Physics.Raycast(ray, out hitInfo))
        {
            transform.position = hitInfo.point;
        }
        else
        {
            transform.position = (ray.origin + ray.direction) * maxRange;
        }

        
    }
}
