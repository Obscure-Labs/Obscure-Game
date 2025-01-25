using System.Collections.Generic;
using MEC;
using UnityEditor;
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

        public GameObject prefab;
        
        public override void Start()
        {
            bulletPrefab = prefab;
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
            print("Started Fire");
        }
        
        public override void FireStop()
        {
            isFiring = false;
        }

        public override void TryFire()
        {
            print($"CanShoot: {canShoot}");
            if (canShoot) Fire();
            print("Attempted Shoot");
        }

        public override void Fire()
        {
            canShoot = false;
            print("did the big shoot");
            Vector2 rot = new Vector2(transform.rotation.x, transform.rotation.y)*bulletSpeed*100f;
            SpawnProjectile(rot, Range);

            Timing.CallDelayed(2000, () => { canShoot = true; });
        }
        
        public override void SpawnProjectile(Vector2 rot, float range)
        {
            GameObject bulletFab = Instantiate(bulletPrefab);
            bulletFab.SetActive(true);
            bulletFab.transform.rotation = Quaternion.Euler(rot);
            List<GameObject> bullets = new List<GameObject>();
            
            for (int i = 0; i < bulletFab.transform.childCount; i++)
            {
                bullets.Add(bulletFab.transform.GetChild(i).gameObject);
            }

            foreach (GameObject i in bullets)
            {
                i.AddComponent<Rigidbody2D>();
                var comp = i.AddComponent<BulletScript>();
                comp.LifeTime = range;
                comp.dir = rot;
            }
            
        }
    }
}