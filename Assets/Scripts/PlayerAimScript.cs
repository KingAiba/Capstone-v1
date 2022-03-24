using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerAimScript : MonoBehaviour
{
    PlayerInputHandler pInput;

    public Rig aimLayer;
    public float aimSpeed = 1f;

    public WeaponHolderScript playerWeaponHolder;

    //public Animator weaponRigContoller;

    public RigBuilder playerRigBuilder;

    public Rig HandIKLayer;

    public void AimWeapon()
    {
        if (pInput.rmbPressed)
        {
            ChangeAimLayerWeight(aimSpeed * Time.deltaTime);
        }

        if (!pInput.rmbPressed && !pInput.lmbPressed)
        {
            ChangeAimLayerWeight(-aimSpeed * Time.deltaTime);
        }
    }
    public void FireWeapon()
    {
        if (pInput.lmbPressed)
        {
            ChangeAimLayerWeight(1);
            playerWeaponHolder.StartFireActiveWeapon();      
        }
        else
        {
            
            playerWeaponHolder.StopFireActiveWeapon();
        }

    }

    public void SwitchWeapon()
    {
        if(pInput.weaponSwitch)
        {
            ChangeWeapon();
            //weaponRigContoller.Play("equip_" + playerWeaponHolder.activeWeapon.weaponType.ToString());
            //Debug.Log("equip_" + playerWeaponHolder.activeWeapon.weaponType.ToString());
        }
    }

    public void ReloadWeapon()
    {
        if(pInput.RPressed)
        {
            playerWeaponHolder.ReloadActiveWeapon();
        }
    }

    public void ChangeAimLayerWeight(float change)
    {
        aimLayer.weight += change;
    }

    public void ChangeWeapon()
    {
        playerWeaponHolder.ChangeToNextWeapon();
        playerWeaponHolder.ResetHolsterPosition();
        playerRigBuilder.Build();
        playerWeaponHolder.ResetHolsterPosition();
        //playerWeaponHolder.ActivateCurrWeapon();
    }
    void Start()
    {
        pInput = GetComponent<PlayerInputHandler>();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        playerRigBuilder = GetComponent<RigBuilder>();
        playerWeaponHolder = GetComponentInChildren<WeaponHolderScript>();
    }


    void Update()
    {
        SwitchWeapon();

        AimWeapon();

        ReloadWeapon();

        FireWeapon();

    }
}
