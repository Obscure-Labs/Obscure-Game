using System.Collections;
using System.Collections.Generic;
using Items;
using MEC;
using UnityEditor;
using UnityEngine;

namespace Items
{
    public abstract class GunBase : Item
    {
        //STATS
        public abstract int Damage { get; set; }
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

        public virtual void SpawnProjectile(Vector2 rot, float range)
        {
            GameObject bulletFab = Instantiate(bulletPrefab);
            bulletFab.transform.rotation = Quaternion.Euler(rot);
            List<GameObject> bullets = new List<GameObject>();
            
            for (int i = 0; i < bulletFab.transform.childCount; i++)
            {
                bullets.Add(bulletFab.transform.GetChild(i).gameObject);
            }

            foreach (GameObject i in bullets)
            {
                var comp = i.AddComponent<BaseBulletScript>();
                comp.LifeTime = range;
                comp.dir = rot;
            }
            
        }
    }

    public class BaseBulletScript : MonoBehaviour
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
