using BadJuja.Core;
using BadJuja.Core.CharacterStats;
using BadJuja.Core.Events;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BadJuja.UI {
    public class UpgradeScreen : MonoBehaviour {

        [SerializeField] private List<StatUpgradeList> listOfUpgrades = new();

        public GameObject CardPrefab;
        public int CardsPerLevel = 3;

        private List<GameObject> cards = new List<GameObject>();
        private List<StatUpgrade> statUpgradeComponents = new();

        private Image backgroundImage;

        private bool cardChoseActive = false;

        private void Start()
        {
            if (backgroundImage == null)
            {
                backgroundImage = GetComponent<Image>();
                backgroundImage.enabled = false;
            }

            if (CardPrefab == null) return;
            
            InstantiateCards();

            PlayerRelatedEvents.OnLevelIncrease += EnableCards;
        }

        public void ApplyModifier(AllCharacterStats stat, StatModType type, float strenght)
        {

            ModifiersRelaterEvents.Send_AddPlayerModifier(stat, new StatModifier(strenght, type, this), this);

            for (int i = 0; i < CardsPerLevel; i++)
            {
                statUpgradeComponents[i].ResetDescription();
                cards[i].SetActive(false);
            }

            backgroundImage.enabled = false;
            cardChoseActive = false;

            Time.timeScale = 1;
        }

        private void InstantiateCards()
        {
            listOfUpgrades.ForEach(list => list.Init());

            for (int i = 0; i < CardsPerLevel; i++)
            {
                GameObject card = Instantiate(CardPrefab, transform);
                
                cards.Add(card);
                statUpgradeComponents.Add(card.GetComponent<StatUpgrade>());
                
                card.SetActive(false);
            }
        }

        private void EnableCards()
        {
            if (cardChoseActive) return;

            var upgrades = GetRandomUpgrades(listOfUpgrades, CardsPerLevel);
            for(int i = 0; i < CardsPerLevel; i++)
            {
                var stat = upgrades[i].CharacterStat;
                var mod = upgrades[i].GetModifier();

                statUpgradeComponents[i].Init(stat, mod.Type, mod.Value);

                cards[i].SetActive(true);
            }

            backgroundImage.enabled = true;
            cardChoseActive = true;
            Time.timeScale = 0;
        }

        private List<T> GetRandomUpgrades<T>(List<T> inputList, int count)
        {
            List<T> listClone = new(inputList);
            Shuffle(listClone);
            return listClone.GetRange(0, count);
        }

        private void Shuffle<T>(List<T> inputList)
        {
            for (int i = 0; i < inputList.Count; i++)
            {
                T temp = inputList[i];
                int rand = Random.Range(i, inputList.Count);
                inputList[i] = inputList[rand];
                inputList[rand] = temp;
            }
        }

    }
}