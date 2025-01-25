using System;
using System.Collections.Generic;
using DefaultNamespace.Items;
using MEC;
using UnityEditor;
using UnityEngine;

namespace Items.Weapons
{
    public class Shotgun : GunBase
    {
        
        
        public override string Name { get; set; } = "Shotgun";
        public override int Id { get; set; } = 1;
        
        // Measured in Hearts
        public override int Damage { get; set; } = 35;
        public override int MagCapacity { get; set; } = 10;
        // Measured in Settings
        public override float ReloadTime { get; set; } = 1.5f;
        // Measured in 
        public override float fireRate { get; set; } = 72f;
        // Measured in Units per second (player walks at 2 units per second and runs at 4)
        public override float bulletSpeed { get; set; } = 12f;
        // Measured in Seconds
        public override float Range { get; set; } = 5f;
        //
        public override Transform bulletSpawnpoint { get; set; }
        public override GameObject bulletPrefab { get; set; }
        public override Weapon Type { get; set; } = Weapon.Shotgun;
        public override int MagCurrent { get; set; } = 10;
        public override bool isFiring { get; set; } = false;
        public override bool canShoot { get; set; } = true;

        public GameObject _prefab;
        public Transform _bulletSpawnPoint;
        
        public override void Start()
        {
            bulletSpawnpoint = _bulletSpawnPoint;
            bulletPrefab = _prefab;
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
            var v3 = Input.mousePosition;
            v3.z = 10.0f;
            v3 = Camera.main.ScreenToWorldPoint(v3);
            Vector2 rot = (v3-transform.position).normalized;
            SpawnProjectile(rot, Range);

            Timing.CallDelayed(60/fireRate, () => { canShoot = true; });
        }
        
        public override void SpawnProjectile(Vector2 rot, float range)
        {
            GameObject bulletFab = Instantiate(bulletPrefab);
            bulletFab.SetActive(true);
            bulletFab.transform.position = _bulletSpawnPoint.position;
            bulletFab.transform.rotation = Quaternion.Euler(rot);
            List<GameObject> bullets = new List<GameObject>();
            
            for (int i = 0; i < bulletFab.transform.childCount; i++)
            {
                bullets.Add(bulletFab.transform.GetChild(i).gameObject);
            }

            foreach (GameObject i in bullets)
            {
                var rb = i.AddComponent<Rigidbody2D>();
                rb.gravityScale = 0;
                rb.drag = 0;
                rb.angularDrag = 0;
                var comp = i.AddComponent<ShotgunBulletScript>();
                comp.LifeTime = range;
                comp.dir = rot;
                comp.speed = bulletSpeed;
                comp.Damage = Damage;
            }
            
        }
    }
    
    public class ShotgunBulletScript : MonoBehaviour
    {
        public float LifeTime;
        private float timer = 0;
        public Vector2 dir;
        public float speed;
        public int Damage;
        public LayerMask mask = 1 << 3 << 2;
        
        void Update()
        {
            print($"Attempting to add force to {gameObject.name}" );
            var rb = GetComponentInParent<Rigidbody2D>();
            rb.velocity = dir*speed;
            if(timer > LifeTime) {Destroy(transform.parent.gameObject); print("Destroying from lifetime");}
            timer += Time.deltaTime;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!other.transform.CompareTag("Player"))
            {
                try
                {
                    other.gameObject.GetComponent<ObjectHealth>().Damage(Damage);
                    Destroy(transform.parent.gameObject);
                }
                catch (NullReferenceException ex)
                {
                    
                }
            }
        }
    }
}