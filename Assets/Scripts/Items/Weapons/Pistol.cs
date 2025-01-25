using MEC;
using UnityEngine;

namespace Items.Weapons
{
    public class Pistol : GunBase
    {
        
        
        public override string Name { get; set; } = "Pistol";
        public override int Id { get; set; } = 0;

        public override float Damage { get; set; } = 1f;
        public override int MagCapacity { get; set; } = 10;
        public override float ReloadTime { get; set; } = 1.5f;
        public override float fireRate { get; set; } = 72f;
        public override float bulletSpeed { get; set; } = 35f;
        public override float Range { get; set; } = 5f;
        public override Transform bulletSpawnpoint { get; set; }
        public override GameObject bulletPrefab { get; set; }
        public override int MagCurrent { get; set; } = 10;
        public override bool isFiring { get; set; } = false;
        public override bool canShoot { get; set; } = true;

        public override void Start()
        {
            canShoot = true;
            MagCurrent = MagCapacity;
        }
        
        public override void Tick()
        {
            if (isFiring) TryFire();
        }
        
        public override void FireStart()
        {
            isFiring = true;
        }
        
        public override void FireStop()
        {
            isFiring = false;
        }

        public override void TryFire()
        {
            if (canShoot) Fire();
        }

        public override void Fire()
        {
            canShoot = false;

        //SHOOT LOGIC HERE

            Timing.CallDelayed(60000 / fireRate, () => { canShoot = true; });
        }
    }
}