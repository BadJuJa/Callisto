using BadJuja.Core.CharacterStats;

namespace BadJuja.Core
{
    public interface IWeapon
    {
        public void DealDamage(IDamagable targetDamagable, IStats targetStats, int enemyCount = 1);
    }
}
