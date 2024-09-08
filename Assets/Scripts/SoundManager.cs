using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance {get;set;}
    public AudioSource shootingChannel;
    public AudioSource reloadChanel;
    public AudioSource shootingSoundM16;
    public AudioSource walkingSound;

    public AudioClip pistonShot;
    public AudioClip m16Shot;

    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void PlayShootSound(Weapon.WeaponModel weapon)
    {
        switch (weapon)
        {
            case Weapon.WeaponModel.Pistol:
                shootingChannel.PlayOneShot(pistonShot);
                break;
            case Weapon.WeaponModel.M16:
                shootingChannel.PlayOneShot(m16Shot);
                break;
            default:
                break;
        }
    }

    public void PlayReloadSound(Weapon.WeaponModel weapon)
    {
        switch (weapon)
        {
            case Weapon.WeaponModel.Pistol:
                reloadChanel.Play();
                break;
            case Weapon.WeaponModel.M16:
                reloadChanel.Play();
                break;
            default:
                break;
        }
    }
}
