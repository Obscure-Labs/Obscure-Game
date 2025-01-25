using UnityEngine;

namespace Items
{
    public abstract class Item : MonoBehaviour
    {
        public abstract string Name { get; set; }
        public abstract int Id { get; set; }

        public virtual void Use()
        {
            
        }
    }
}