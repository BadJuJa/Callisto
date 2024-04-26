using BadJuja.Core.CharacterStats;
using BadJuja.Core;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatUpgrade : MonoBehaviour
{
    public AllCharacterStats Stat;
    public StatModType ModType;
    public float Strenght;

    private UpgradeScreen upgradeScreen;
    [SerializeField] private TextMeshProUGUI _text;

    private Dictionary<AllCharacterStats, string> statTranslationDict = new()
    {
        { AllCharacterStats.Health, "здоровье" },
        { AllCharacterStats.Armor, "броню" },
        { AllCharacterStats.MovementSpeed, "скорость передвижения" },
        { AllCharacterStats.Damage, "урон" },
        { AllCharacterStats.CritChance, "шанс критической атаки" },
        { AllCharacterStats.CritDamage, "критический урон" },
        { AllCharacterStats.FireDamage, "урон огнём" },
        { AllCharacterStats.FrostDamage, "урон холодом" },
        { AllCharacterStats.ShockDamage, "урон электричеством" },
    };

    private void Awake()
    {
        upgradeScreen = GetComponentInParent<UpgradeScreen>();

        if (_text == null)
            _text = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void Clicked()
    {
        if (upgradeScreen == null) return;
        
        upgradeScreen.ApplyModifier(Stat, ModType, Strenght);
    }

    public void UpdateDescription()
    {
        if (_text == null) return;

        string text = _text.text
            .Replace("<upgradeStat>", statTranslationDict[Stat])
            .Replace("<strenght>", Strenght.ToString())
            .Replace("<modType>", ModType == StatModType.Flat ? "" : ModType == StatModType.PercentAdd ? "% аддитивно" : "% мультипликативно");
        _text.SetText(text);
    }

    public void Init(AllCharacterStats stat, StatModType type, float strenght)
    {
        Stat = stat;
        ModType = type;
        if (type == StatModType.PercentAdd || type == StatModType.PercentMult)
            Strenght = strenght / 100;

        //else Strenght = stat == AllCharacterStats.DamageReduction ? strenght / 5 : strenght;

        UpdateDescription();
    }
}
