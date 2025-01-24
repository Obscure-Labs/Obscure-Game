using System.Collections;
using System.Collections.Generic;
using Items;
using MEC;
using UnityEngine;

public class GunBase : Item
{
    //STATS
    public float Damage { get; set; }
    public int MagCapacity { get; set; }
    public float ReloadTime { get; set; }
    public float fireRate { get; set; }
    public float bulletSpeed { get; set; }
    public float Range { get; set; }
    
    //CURRENT PARAMETERS
    public float MagCurrent { get; set; }
    public bool isFiring { get; set; }
    public bool canShoot { get; set; } = true;

    public virtual void Start()
    {
        MagCurrent = MagCapacity;
    }
    
    public virtual void Tick()
    {
        if(isFiring) TryFire();
        
    }
    
    public virtual void FireStart()
    {
        isFiring = true;
    }

    public virtual void FireStop()
    {
        isFiring = false;
    }

    public virtual void TryFire()
    {
        if(canShoot) Fire();
    }

    public virtual void Fire()
    {
        canShoot = false;
        
        //SHOOT LOGIC HERE
        
        Timing.CallDelayed(60000 / fireRate, () => { canShoot = true; });
    }
}
