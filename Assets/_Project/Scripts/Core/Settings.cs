using UnityEngine;

namespace BadJuja.Core.Settings
{

    public enum DiffecultyLevel
    {
        Easy,
        Normal,
        Hard,
    }

    [CreateAssetMenu(fileName = "Game Settings", menuName = "Game Settings")]
    public class Settings : ScriptableObject
    {
        public DiffecultyLevel DiffecultyLevel = DiffecultyLevel.Normal;
    }
}
