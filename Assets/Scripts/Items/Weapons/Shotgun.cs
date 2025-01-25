using System.Collections.Generic;
using MEC;
using UnityEditor;
using UnityEngine;

namespace Items.Weapons
{
    public class Shotgun : GunBase
    {


        public override string Name { get; set; } = "Pistol";
        public override int Id { get; set; } = 0;

        public override int Damage { get; set; } = 15;
        public override int MagCapacity { get; set; } = 6;
        public override float ReloadTime { get; set; } = 2.5f;
        public override float fireRate { get; set; } = 272f;
        public override float bulletSpeed { get; set; } = 35f;
        public override float Range { get; set; } = 5f;
        public override Transform bulletSpawnpoint { get; set; }
        public override GameObject bulletPrefab { get; set; }
        public override int MagCurrent { get; set; } = 6;
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
            Vector2 rot = new Vector2(transform.rotation.x, transform.rotation.y) * bulletSpeed * 100f;
            SpawnProjectile(rot, Range);

            Timing.CallDelayed(2000, () => { canShoot = true; });
        }

        public override void SpawnProjectile(Vector2 rot, float range)
        {
            GameObject bulletFab = Instantiate(bulletPrefab);
            bulletFab.SetActive(true);
            bulletFab.transform.rotation = Quaternion.Euler(rot);
            List<GameObject> bullets = new List<GameObject>();

            for (int i = 0; i < bulletFab.transform.childCount + 2; i++)
            {
                bullets.Add(bulletFab.transform.GetChild(i).gameObject);
            }

            foreach (GameObject i in bullets)
            {
                i.AddComponent<Rigidbody2D>();
                var comp = i.AddComponent<ShotgunBulletScript>();
                comp.LifeTime = range;
                comp.dir = rot += new Vector2(Random.Range(-45, 45), (Random.Range(-45, 45)));
            }

        }
    }
    
    public class ShotgunBulletScript : MonoBehaviour
    {
        public float LifeTime;
        private float timer = 0;
        public Vector2 dir;
        public LayerMask mask = 1 << 3;
        
        void Update()
        {
            print($"Attempting to add force to {gameObject.name}" );
            GetComponent<Rigidbody2D>().AddForce(dir*20f);
            Physics.Raycast(gameObject.transform.position,
                transform.forward + (transform.up * UnityEngine.Random.Range(-0.15f, 0.15f)) +
                (transform.right * UnityEngine.Random.Range(-0.15f, 0.15f)), out RaycastHit hitInfo, 1, mask);
            // if (hitInfo.rigidbody == null)
            // {
            //     Destroy(transform.parent);
            // }
            if(timer > LifeTime*1000) Destroy(transform.parent);
            timer += Time.deltaTime;
        }
    }
}