using BadJuja.Core;
using BadJuja.Core.CharacterStats;
using BadJuja.Core.Events;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace BadJuja.UI.PauseMenu {
    public class PlayerStats : MonoBehaviour
    {
        public GameObject PlayerStatPrefab;

        private IStats playerStats;

        private Dictionary<AllCharacterStats, PlayerStatsElement> elementDict;

        public void Initialize()
        {
            elementDict = new();

            foreach (AllCharacterStats stat in Enum.GetValues(typeof(AllCharacterStats)))
            {
                var element = Instantiate(PlayerStatPrefab, transform).GetComponent<PlayerStatsElement>();
                element.SetName(stat.ToString());
                element.SetValue("");
                elementDict.Add(stat, element);
            }

            GameRelatedEvents.OnPause += RefreshValues;
        }

        private void RefreshValues()
        {
            playerStats ??= GameObject.FindGameObjectWithTag("Player").GetComponent<IStats>();

            foreach (AllCharacterStats stat in Enum.GetValues(typeof(AllCharacterStats)))
            {
                float _ = playerStats.GetStatValue(stat);
                string statValue = _ > 0 ? $"+{_}" : $"{_}";

                if (!(stat == AllCharacterStats.Health
                    || stat == AllCharacterStats.Armor
                    || stat == AllCharacterStats.Damage
                    || stat == AllCharacterStats.MovementSpeed))
                    statValue += "%";

                elementDict[stat].SetValue(statValue);
            }
        }
    }
}
