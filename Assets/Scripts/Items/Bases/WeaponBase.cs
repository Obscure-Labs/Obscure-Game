using System.Collections;
using System.Collections.Generic;
using Items;
using MEC;
using UnityEngine;

namespace Items
{
    public class GunBase : Item
    {
        //STATS
        public static float Damage { get; set; }
        public static int MagCapacity { get; set; }
        public static float ReloadTime { get; set; }
        public static float fireRate { get; set; }
        public static float bulletSpeed { get; set; }
        public static float Range { get; set; }

        //CURRENT PARAMETERS
        public static float MagCurrent { get; set; }
        public static bool isFiring { get; set; }
        public static bool canShoot { get; set; } = true;

        public virtual void Start()
        {
            MagCurrent = MagCapacity;
        }

        public virtual void Tick()
        {
            if (isFiring) TryFire();

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
            if (canShoot) Fire();
        }

        public virtual void Fire()
        {
            canShoot = false;

            //SHOOT LOGIC HERE

            Timing.CallDelayed(60000 / fireRate, () => { canShoot = true; });
        }
    }
}
