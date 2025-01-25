using System;
using System.Collections.Generic;
using DefaultNamespace.Items;
using MEC;
using UnityEditor;
using UnityEngine;

namespace Items.Weapons
{
    public class Revolver : GunBase
    {
        
        
        public override string Name { get; set; } = "Revolver";
        public override int Id { get; set; } = 5;
        
        // Measured in Hearts
        public override int Damage { get; set; } = 80;
        public override int MagCapacity { get; set; } = 6;
        // Measured in Settings
        public override float ReloadTime { get; set; } = 3f;
        // Measured in 
        public override float fireRate { get; set; } = 50f;
        // Measured in Units per second (player walks at 2 units per second and runs at 4)
        public override float bulletSpeed { get; set; } = 200f;
        // Measured in Seconds
        public override float Range { get; set; } = 1.5f;
        //
        public override Transform bulletSpawnpoint { get; set; }
        public override GameObject bulletPrefab { get; set; }
        public override Weapon Type { get; set; } = Weapon.Revolver;
        public override int MagCurrent { get; set; } = 6;
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
            if (MagCurrent > 0)
            {
                MagCurrent--;
            }
            else
            {
                Timing.CallDelayed(ReloadTime, () => { MagCurrent = MagCapacity; });

                return;
            }
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
                var comp = i.AddComponent<RevolverBulletScript>();
                comp.LifeTime = range;
                comp.dir = rot;
                comp.speed = bulletSpeed;
                comp.Damage = Damage;
            }
            
        }
    }
    
    public class RevolverBulletScript : MonoBehaviour
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