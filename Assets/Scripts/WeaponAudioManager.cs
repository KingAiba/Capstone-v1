using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAudioManager : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip shootAudio;
    public AudioClip reloadAudio;

    public WeaponRayCastScript weapon;


    public void PlayShootAudio()
    {
        audioSource.clip = shootAudio;
        audioSource.Play();
    }

    public void PlayReloadAudio()
    {
        audioSource.clip = reloadAudio;
        audioSource.Play();
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        weapon = GetComponent<WeaponRayCastScript>();

        weapon.OnWeaponShoot += PlayShootAudio;
        weapon.OnReload += PlayReloadAudio;
    }

}
