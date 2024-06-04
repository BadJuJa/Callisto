namespace BadJuja.Core
{
    public enum AllCharacterStats {
        Health,
        Armor,
        MovementSpeed,
        Damage,
        CritChance,
        CritDamage,
        FireDamage,
        FrostDamage,
        ShockDamage,
        FireResistance,
        FrostResistance,
        ShockResistance,
    }

    public enum EnemyTypes {
        Melee,
        Range,
    }

    public enum PlayerWeaponTypes {
        Melee,
        Light,
        Heavy,
    }

    public enum WeaponElements
    {
        Fire,
        Frost,
        Shock,
        Physical
    }

    public enum RangedWeaponBehaviour
    {
        FiringForward,
        FiringBackward,
        NearestTarget,
    }

    public enum MeleeWeaponBehaviour
    {
        CircularMotion,
        AreaDamage,
    }
}
