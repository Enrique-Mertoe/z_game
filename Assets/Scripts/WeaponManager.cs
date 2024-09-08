using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager Instance { get; set; }
    public GameObject activeWeaponSlot;

    public List<GameObject> weaponSlots;

    public void Awake()
    {
        if (!Instance && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        activeWeaponSlot = weaponSlots[0];
    }

    private void Update()
    {
        foreach (var weaponSlot in weaponSlots)
        {
            weaponSlot.SetActive(weaponSlot == activeWeaponSlot);
        }
    }

    public void PickUpWeapon(GameObject pickedWeapon)
    {
        AddWeaponIntoActiveSlot(pickedWeapon);
    }

    private void AddWeaponIntoActiveSlot(GameObject weapon)
    {
        weapon.transform.SetParent(activeWeaponSlot.transform,false);
        var nWeapon = weapon.GetComponent<Weapon>();
        var spnP = nWeapon.spawnPosition;
        var spnR = nWeapon.spawnRotation;
        weapon.transform.localPosition = new Vector3(spnP.x, spnP.y, spnP.z);
        weapon.transform.localRotation = Quaternion.Euler(spnR.x,spnR.y,spnR.z);
        nWeapon.isActiveWeapon = true;

    }
}