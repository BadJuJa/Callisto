using BadJuja.Core.CharacterStats;
using BadJuja.Core;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace BadJuja.UI {
    public class StatUpgrade : MonoBehaviour {
        public AllCharacterStats Stat;
        public StatModType ModType;
        public float Strenght;

        private UpgradeScreen upgradeScreen;
        [SerializeField] private TextMeshProUGUI _text;
        private string _originalText;

        [SerializeField]
        private Dictionary<AllCharacterStats, string> statTranslationDict = new()
    {
        { AllCharacterStats.Health, "health" },
        { AllCharacterStats.Armor, "armor" },
        { AllCharacterStats.MovementSpeed, "movement speed" },
        { AllCharacterStats.Damage, "all damage" },
        { AllCharacterStats.CritChance, "critical chance" },
        { AllCharacterStats.CritDamage, "critical damage" },
        { AllCharacterStats.FireDamage, "fire damage" },
        { AllCharacterStats.FrostDamage, "frost damage" },
        { AllCharacterStats.ShockDamage, "shock damage" },
        { AllCharacterStats.FireResistance, "fire resistance" },
        { AllCharacterStats.FrostResistance, "frost resistance" },
        { AllCharacterStats.ShockResistance, "shock resistance" },
    };

        private void Awake()
        {
            upgradeScreen = GetComponentInParent<UpgradeScreen>();

            if (_text == null)
                _text = GetComponentInChildren<TextMeshProUGUI>();

            _originalText = _text.text;
        }
        public void Init(AllCharacterStats stat, StatModType type, float strenght)
        {
            Stat = stat;
            ModType = type;
            Strenght = strenght;

            UpdateDescription();
        }

        public void Clicked()
        {
            if (upgradeScreen == null) return;
            if (Stat == AllCharacterStats.MovementSpeed)
                upgradeScreen.ApplyModifier(Stat, StatModType.PercentAdd, Strenght / 100);
            else
                upgradeScreen.ApplyModifier(Stat, ModType, Strenght);
        }

        private void UpdateDescription()
        {
            if (_text == null) return;

            string text = _text.text
                .Replace("<upgradeStat>", statTranslationDict[Stat])
                .Replace("<strenght>", Strenght.ToString())
                .Replace("<modType>", (Stat == AllCharacterStats.Health || Stat == AllCharacterStats.Armor) ? "" : "%");

            _text.SetText(text);
        }

        public void ResetDescription()
        {
            _text.SetText(_originalText);
        }
    }
}
