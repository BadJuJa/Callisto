using TMPro;
using UnityEngine;

namespace BadJuja.UI.PauseMenu {
    public class PlayerStatsElement : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI Name;
        [SerializeField] private TextMeshProUGUI Value;

        public void SetName(string name) => Name.SetText(name);

        public void SetValue(string value) => Value.SetText(value);
    }
}
