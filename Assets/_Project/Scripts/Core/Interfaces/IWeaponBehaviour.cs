using System.Collections.Generic;

namespace BadJuja.Core
{
    public interface IWeaponBehaviour
    {
        public void Upgrade(Dictionary<string, float> args);
    }
}
