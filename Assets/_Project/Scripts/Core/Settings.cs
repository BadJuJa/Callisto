using UnityEngine;

namespace BadJuja.Core.Settings
{

    public enum DifficultyLevel
    {
        Easy,
        Normal,
        Hard,
    }

    [CreateAssetMenu(fileName = "Game Settings", menuName = "Game Settings")]
    public class Settings : ScriptableObject
    {
        public DifficultyLevel DiffecultyLevel = DifficultyLevel.Normal;
    }
}
