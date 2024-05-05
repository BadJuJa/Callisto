using UnityEngine;

namespace BadJuja.Core.Data
{
    [CreateAssetMenu(fileName = "Player Current Equipment", menuName = "Data/Player Current Equipment")]
    public class PlayerCurrentEquipment : ScriptableObject
    {
        [Header("Model")]
        public GameObject Model;

        [Header("Light Weapon")]
        public RangedWeaponData LightRangedWeaponData;
        public ElementData LightRangedWeaponElement;

        [Header("Heavy Weapon")]
        public RangedWeaponData HeavyRangedWeaponData;
        public ElementData HeavyRangedWeaponElement;

        [Header("Melee Weapon")]
        public MeleeWeaponData MeleeWeaponData;
        public ElementData MeleeWeaponElement;

        [Header("Character")]
        public Lists.CharacterModifierPreset CharacterBonusModifiers;
    }
}
