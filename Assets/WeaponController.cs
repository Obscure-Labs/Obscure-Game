using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Items;
using Items.Weapons;

public enum Weapon
{
    Pistol,
    Minigun,
    Shotgun,
    AssaultRifle
    Revolver
}

public class WeaponController : MonoBehaviour
{
    public GunBase currentWeapon { get; set; }

    public List<GunBase> weaponList;

    void Start()
    {
        currentWeapon = weaponList.FirstOrDefault(x => x.Id == 0);
    }

    public void FireWeapon()
    {
        currentWeapon.FireStart();
    }

    public void StopWeapon()
    {
        currentWeapon.FireStop();
    }

    void Update()
    {
        currentWeapon.Tick();
    }

    public void SwitchWeapon(Weapon type)
    {
        print(type.ToString());
        currentWeapon = weaponList.FirstOrDefault(x => x.Type == type);
        currentWeapon.gameObject.SetActive(true);
        foreach (GunBase i in weaponList)
        {
            if (i.Id != currentWeapon.Id) i.gameObject.SetActive(false);
        }
    }
}
