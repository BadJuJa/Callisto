using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace BadJuja.UI.PauseMenu
{
    public class EquipmentItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public TextMeshProUGUI EquipText;
        public Image EquipmentImage;

        private string _statsString = "";

        public void Initialize(Sprite weaponSprite, Dictionary<string, string> stats)
        {
            EquipmentImage.sprite = weaponSprite;

            foreach (KeyValuePair<string, string> kvp in stats)
            {
                _statsString += $"{kvp.Key}: {kvp.Value}\n";
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            EquipText.SetText(_statsString);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            EquipText.SetText("");
        }
    }
}
