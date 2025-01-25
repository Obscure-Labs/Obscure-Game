using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;
using Items.Weapons;

public class WeaponController : MonoBehaviour
{
    public GunBase currentWeapon { get; set; }

    public void Start()
    {
        currentWeapon = GetComponentInChildren<Pistol>();
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
}
