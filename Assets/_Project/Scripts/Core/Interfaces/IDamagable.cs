using BadJuja.Core.CharacterStats;

namespace BadJuja.Core {
    public interface IDamagable {
        public float GetFireResistance { get; }
        public float GetFrostResistance { get; }
        public float GetShockResistance { get; }
        public void TakeDamage(float value);
    }
}