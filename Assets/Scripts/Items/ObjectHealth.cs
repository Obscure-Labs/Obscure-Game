using UnityEngine;

namespace DefaultNamespace.Items
{
    public class ObjectHealth : MonoBehaviour
    {
        public int Health = 100;

        public void Damage(int damage)
        {
            Health -= damage;
        }
        void Start()
        {
            
        }

        void Update()
        {
            if (Health < 0)
            {
                Destroy(gameObject);
            }
        }
    }
}