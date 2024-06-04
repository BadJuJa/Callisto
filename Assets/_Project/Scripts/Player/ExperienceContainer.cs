using BadJuja.Core.Events;
using UnityEngine;

namespace BadJuja.Player {
    public class ExperienceContainer : MonoBehaviour {

        private int _level = 1;
        private int _currentExperience = 0;
        private int _baseXpForLevel = 100;
        private float _xpForLevelMult = 1.25f;

        private int XpToNextLevel {
            get {
                return (int)(_level * _baseXpForLevel * _xpForLevelMult);
            }
        }
        
        public void Initialize() => EnemyRelatedEvents.OnEnemyDied += AddExperience;
        private void OnDestroy() => EnemyRelatedEvents.OnEnemyDied -= AddExperience;
        
        private void AddExperience(int xpValue)
        {
            _currentExperience += xpValue;
            if (_currentExperience >= XpToNextLevel)
            {
                _currentExperience -= XpToNextLevel;
                _level++;
                PlayerRelatedEvents.Send_OnLevelIncreased();
            }
            PlayerRelatedEvents.Send_OnExperienceChanged(_currentExperience, XpToNextLevel);
        }
    }
}