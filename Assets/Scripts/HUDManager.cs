using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public static HUDManager Instance { get; set; }

    [Header("Ammo")] public TextMeshProUGUI magazineAmmoUI;
    public TextMeshProUGUI totalAmmoUI;
    public UnityEngine.UIElements.Image AmmoTypeUI;

    [Header("Weapon")] public Image activeWeaponUI;
    public Image unActiveWeaponUI;

    [Header("Throwables")] public Image lethalUI;
    public TextMeshProUGUI lethalAmountUI;

    public Image tacticalUI;
    public TextMeshProUGUI tacticalAmountUI;

    public Sprite emptySlot;

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

    // private void Update()
    // {
    //     var active = WeaponManager.Instance.activeWeaponSlot.GetComponentInChildren<Weapon>();
    //     var unActive = GetUnActiveWeaponSlot().GetComponentInChildren<Weapon>();
    //     if (active)
    //     {
    //         magazineAmmoUI.text = $"{56}";
    //         // totalAmmoUI.text = GetUnActiveWeaponSlot().GetComponentInChildren<Weapon>();
    //         var model = active.weaponModel;
    //         AmmoTypeUI.sprite = GetAmmoSprite(model);
    //         if (unActive)
    //         {
    //             unActiveWeaponUI.sprite = GetWeaponSprite(unActive.weaponModel);
    //         }
    //     }
    //     else
    //     {
    //         magazineAmmoUI.text = "";
    //         totalAmmoUI.text = "";
    //         AmmoTypeUI.sprite = emptySlot;
    //         activeWeaponUI.sprite = emptySlot;
    //         unActiveWeaponUI.sprite = emptySlot;
    //     }
    // }

    private Sprite GetWeaponSprite(Weapon.WeaponModel model)
    {
        return model switch
        {
            Weapon.WeaponModel.Pistol=>
                Instantiate(Resources.Load<GameObject>("Pistol")).GetComponent<SpriteRenderer>().sprite,
            Weapon.WeaponModel.M16=>
                Instantiate(Resources.Load<GameObject>("M16")).GetComponent<SpriteRenderer>().sprite,
            _=>null
        };

    }

    private Sprite GetAmmoSprite(Weapon.WeaponModel model)
    {
        return model switch
        {
            Weapon.WeaponModel.Pistol=>
                Instantiate(Resources.Load<GameObject>("Pistol_Ammo")).GetComponent<SpriteRenderer>().sprite,
            Weapon.WeaponModel.M16=>
                Instantiate(Resources.Load<GameObject>("M16_Ammo")).GetComponent<SpriteRenderer>().sprite,
            _=>null
        };
    }

    private GameObject GetUnActiveWeaponSlot()
    {
        foreach (var weaponSlot in WeaponManager.Instance.weaponSlots)
        {
            if (weaponSlot != WeaponManager.Instance.activeWeaponSlot)
            {
                return weaponSlot;
            }
        }

        return null;
    }
}