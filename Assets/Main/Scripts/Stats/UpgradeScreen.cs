using BadJuja.Core.CharacterStats;
using BadJuja.Core;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BadJuja.Player;

public class UpgradeScreen : MonoBehaviour
{
    public BadJuja.Player.Player Player;
    public GameObject CardPrefab;

    private List<GameObject> cards = new List<GameObject>();
    private List<GameObject> activeCards = new List<GameObject>();

    private Image backgroundImage;

    private bool cardChoseActive = false;

    private void Start()
    {
        GlobalEvents.OnPlayerLevelIncreased += EnableRandomCards;

        if (Player == null)
        {
            GameObject.FindGameObjectWithTag("Player").TryGetComponent(out Player);
        }

        if (backgroundImage == null)
        {
            backgroundImage = GetComponent<Image>();
            backgroundImage.enabled = false;
        }

        if (CardPrefab != null)
        {
            InstantiateCards();
        }
    }

    public void ApplyModifier(AllCharacterStats stat, StatModType type, float strenght)
    {
        if (Player == null || CardPrefab == null) return;

        Player.ApplyStatMod(stat, type, strenght, this);

        activeCards.ForEach(card => card.SetActive(false));
        backgroundImage.enabled = false;
        cardChoseActive = false;

        Time.timeScale = 1;
    }

    private void InstantiateCards()
    {
        var charStats = System.Enum.GetValues(typeof(AllCharacterStats));
        var modTypes = System.Enum.GetValues(typeof(StatModType));

        foreach (var stat in charStats)
        {
            foreach (var T in modTypes)
            {
                GameObject card = Instantiate(CardPrefab, transform);
                StatUpgrade statUpgrade = card.GetComponent<StatUpgrade>();

                statUpgrade.Init((AllCharacterStats)stat, (StatModType)T, Random.Range(10, 20));

                cards.Add(card);
                card.SetActive(false);
            }
        }
    }

    private void EnableRandomCards()
    {
        if (cardChoseActive) return;

        activeCards = GetUniqueRandomElements(cards, 3);
        activeCards.ForEach(card => card.SetActive(true));
        backgroundImage.enabled = true;
        cardChoseActive = true;
        Time.timeScale = 0;
    }

    private List<T> GetUniqueRandomElements<T>(List<T> inputList, int count)
    {
        List<T> cardListClone = new List<T>(inputList);
        Shuffle(cardListClone);
        return cardListClone.GetRange(0, count);
    }

    private void Shuffle<T>(List<T> inputList)
    {
        for (int i = 0; i < inputList.Count - 1; i++)
        {
            T temp = inputList[i];
            int rand = Random.Range(i, inputList.Count);
            inputList[i] = inputList[rand];
            inputList[rand] = temp;
        }
    }

}
