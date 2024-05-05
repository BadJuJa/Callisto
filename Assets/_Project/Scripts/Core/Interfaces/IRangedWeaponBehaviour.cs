using UnityEngine;

namespace BadJuja.Core
{
    public interface IRangedWeaponBehaviour : IWeaponBehaviour
    {
        public Vector3 GetFiringPoint();
        public Quaternion GetFiringDirection();
    }
}
