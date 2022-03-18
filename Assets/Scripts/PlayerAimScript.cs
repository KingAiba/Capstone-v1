using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerAimScript : MonoBehaviour
{
    PlayerInputHandler pInput;

    Camera playerCamera;
    public Rig aimLayer;

    public float aimSpeed = 1f;
    void Start()
    {
        pInput = GetComponent<PlayerInputHandler>();
        playerCamera = Camera.main;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {  
        if(pInput.rmbPressed)
        {
            aimLayer.weight += aimSpeed * Time.deltaTime;
        }
        else
        {
            aimLayer.weight -= aimSpeed * Time.deltaTime;
        }
    }

    private void LateUpdate()
    {
        
    }
}
