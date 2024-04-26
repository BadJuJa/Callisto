using UnityEngine;

namespace BadJuja.Core.Data
{
    public abstract class WeaponData : ScriptableObject
    {
        public string Name;
        public Sprite Sprite;
        public GameObject Model;

        public float Radius;
        public int Damage;
    }
}
