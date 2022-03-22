using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class WeaponHolderScript : MonoBehaviour
{
    [SerializeField]
    private List<WeaponRayCastScript> weaponList;

    public int currActiveWeapon = 0;
    public WeaponRayCastScript activeWeapon = null;

    public Vector3 defaultHolsterTransform;
    public Transform weaponHolster;
    public Transform weaponPivot;

    public TwoBoneIKConstraint rightHandIK;
    public TwoBoneIKConstraint leftHandIK;

    void Start()
    {
        GetAllWeapon();
        defaultHolsterTransform = weaponHolster.localPosition;
    }

    
    void Update()
    {
        UpdateWeapons();
    }

    public void GetAllWeapon()
    {
        weaponList = new List<WeaponRayCastScript>(GetComponentsInChildren<WeaponRayCastScript>());
        //ChangeToNextWeapon();
        PickWeapon(0);
    }

    public void ChangeToNextWeapon()
    { 
        PickWeapon((currActiveWeapon + 1) % weaponList.Count);
        rightHandIK.data.target = activeWeapon.rightHandGripIK;
        leftHandIK.data.target = activeWeapon.leftHandGripIK;
        ActivateCurrWeapon();

    }

    public void PickWeapon(int pickedWeapon)
    {
        currActiveWeapon = pickedWeapon;
        activeWeapon = weaponList[pickedWeapon];    
    }

    public void ActivateCurrWeapon()
    {
        for(int i = 0; i < weaponList.Count; i++)
        {
            if(i == currActiveWeapon)
            {
                //weaponList[i].gameObject.SetActive(true);
                AimWeapon(weaponList[i]);
            }
            else
            {
                //weaponList[i].gameObject.SetActive(false);
                HolsterWeapon(weaponList[i]);
            }
        }
    }

    public void HolsterWeapon(WeaponRayCastScript weapon)
    {
        weapon.transform.SetParent(weaponHolster, false);
        //weaponHolster.localPosition = defaultHolsterTransform;
    }

    public void ResetHolsterPosition()
    {
        weaponHolster.localPosition = weaponHolster.localPosition - (weaponHolster.localPosition - defaultHolsterTransform);
    }

    public void AimWeapon(WeaponRayCastScript weapon)
    {
        weapon.transform.SetParent(weaponPivot, false);
    }

    public void StartFireActiveWeapon()
    {
        activeWeapon.StartFiring();
        activeWeapon.UpdateFiring(Time.deltaTime);
    }

    public void StopFireActiveWeapon()
    {
        activeWeapon.StopFiring();
    }

    public void UpdateWeapons()
    {
        foreach(WeaponRayCastScript weapon in weaponList)
        {
            weapon.UpdateBullets();
        }       
    }
}
