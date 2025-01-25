using System.Collections;
using System.Collections.Generic;
using Items;
using MEC;
using UnityEngine;

namespace Items
{
    public abstract class GunBase : Item
    {
        //STATS
        public abstract float Damage { get; set; }
        public abstract int MagCapacity { get; set; }
        public abstract float ReloadTime { get; set; }
        public abstract float fireRate { get; set; }
        public abstract float bulletSpeed { get; set; }
        public abstract float Range { get; set; }
        public abstract Transform bulletSpawnpoint { get; set; }
        public abstract GameObject bulletPrefab { get; set; }

        //CURRENT PARAMETERS
        public abstract int MagCurrent { get; set; }
        public abstract bool isFiring { get; set; }
        public abstract bool canShoot { get; set; }

        public virtual void Start()
        {
            
        }

        public virtual void Tick()
        {
            if (isFiring) TryFire();

        }

        public virtual void FireStart()
        {
            
        }

        public virtual void FireStop()
        {
            
        }

        public virtual void TryFire()
        {
            
        }

        public virtual void Fire()
        {
            
        }

        public virtual void SpawnProjectile(Vector2 initialVel, Vector2 constantVel, float range)
        {
            
        }
    }
}
