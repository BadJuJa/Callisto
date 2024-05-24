namespace BadJuja.Core.CharacterStats
{
    public interface IStats
    {
        public float GetStatValue(AllCharacterStats stat);
        public float GetElementalResistance(WeaponElements damageElement);
        public float GetElementalBonus(WeaponElements damageElement);
    }
}
