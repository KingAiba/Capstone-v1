using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerUIHandler : MonoBehaviour
{
    public playerController player;
    public PlayerAimScript aimScript;
    public WeaponHolderScript playerWeaponHolder;

    public FillUI reloadRadialFill;
    public TextMeshProUGUI ammoText;
    public FillUI playerHealth;

    public void EnableFill()
    {
        reloadRadialFill.gameObject.SetActive(true);
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<playerController>();
        aimScript = player.GetComponent<PlayerAimScript>();
        playerWeaponHolder = aimScript.playerWeaponHolder;

        playerWeaponHolder.OnActiveWeaponReload += EnableFill;
    }

    private void LateUpdate()
    {
        reloadRadialFill.UpdateFill(playerWeaponHolder.GetCurrentWeapon().currReloadTimer / playerWeaponHolder.GetCurrentWeapon().reloadTime);
        ammoText.SetText("" + playerWeaponHolder.activeWeapon.currMagAmount);
        playerHealth.UpdateBarFill(player.currHealth / player.maxHealth);
    }

    private void OnDestroy()
    {
        aimScript.playerWeaponHolder.OnActiveWeaponReload -= EnableFill;
    }
}
