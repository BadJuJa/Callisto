using BadJuja.Core;
using BadJuja.Core.Data;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BadJuja.UI.PauseMenu {
    public class Equipment : MonoBehaviour
    {
        public GameObject EquipmentItemTemplate;
        public PlayerCurrentEquipment PlayerEquipment;

        private void Start()
        {
            EquipmentItem component;
            Dictionary<string, string> dict = new();

            // char
            GameObject obj = Instantiate(EquipmentItemTemplate, transform);
            component = obj.GetComponent<EquipmentItem>();
            foreach(var mod in PlayerEquipment.CharacterBonusModifiers.CharacterModifiers)
            {
                string value = "";

                if (mod.Strenght > 0) value += "+";

                value += mod.Strenght.ToString();

                if (!(mod.Stat == AllCharacterStats.Health || mod.Stat == AllCharacterStats.Armor))
                    value += "%";

                dict.Add(mod.Stat.ToString(), value);
            }
            component.Initialize(PlayerEquipment.MeleeWeaponData.Sprite, dict);
            obj.SetActive(true);
            dict.Clear();

            // melee
            component = Item(PlayerEquipment.MeleeWeaponData, PlayerEquipment.MeleeWeaponElement, ref dict);
            component.Initialize(PlayerEquipment.MeleeWeaponData.Sprite, dict);
            dict.Clear();

            // ranged light
            component = Item(PlayerEquipment.LightRangedWeaponData, PlayerEquipment.LightRangedWeaponElement, ref dict);
            dict.Add("Fire Rate", PlayerEquipment.LightRangedWeaponData.FireRate.ToString());
            component.Initialize(PlayerEquipment.LightRangedWeaponData.Sprite, dict);
            dict.Clear();

            // ranged heavy
            component = Item(PlayerEquipment.HeavyRangedWeaponData, PlayerEquipment.HeavyRangedWeaponElement, ref dict);
            dict.Add("Fire Rate", PlayerEquipment.HeavyRangedWeaponData.FireRate.ToString());
            component.Initialize(PlayerEquipment.HeavyRangedWeaponData.Sprite, dict);
            dict.Clear();
        }

        private EquipmentItem Item(WeaponData data, ElementData element, ref Dictionary<string, string> dict)
        {
            GameObject obj = Instantiate(EquipmentItemTemplate, transform);
            obj.SetActive(true);
            obj.GetComponent<Image>().color = element.Color;

            EquipmentItem component = obj.GetComponent<EquipmentItem>();

            dict.Add("Damage", data.Damage.ToString());
            dict.Add("Radius", data.Radius.ToString());
            return component;
        }
    }
}
