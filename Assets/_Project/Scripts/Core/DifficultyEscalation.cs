using BadJuja.Core.Data;
using BadJuja.Core.Data.Lists;
using System.Collections.Generic;
using UnityEngine;

namespace BadJuja.Core {
    [CreateAssetMenu(fileName = "New Difficulty Escalator", menuName = "Difficulty Escalation")]
    public class DifficultyEscalation : ScriptableObject {
        public Settings.Settings Settings;

        public EnemyList EasyEnemyList;
        public EnemyList NormalEnemyList;
        public EnemyList HardEnemyList;

        public List<EnemyData> GetNextRoomEnemies(int roomIndex)
        {
            int count = 5 + roomIndex;

            int easyCount = 0;
            int normalCount = 0;
            int hardCount = 0;
            switch (Settings.DiffecultyLevel)
            {
                case Core.Settings.DifficultyLevel.Easy:
                    easyCount = Mathf.RoundToInt(count * 0.7f);
                    normalCount = count - easyCount;
                    break;
                case Core.Settings.DifficultyLevel.Normal:
                    normalCount = Mathf.RoundToInt(count * 0.6f);
                    easyCount = Mathf.RoundToInt(count * 0.2f);
                    hardCount = count - normalCount - easyCount;
                    break;
                case Core.Settings.DifficultyLevel.Hard:
                    hardCount = Mathf.RoundToInt(count * 0.7f);
                    normalCount = count - hardCount;
                    break;
            }
            List<EnemyData> result = new();
            if (easyCount > 0)
                result.AddRange(EasyEnemyList.GetRandomEnemies(easyCount));
            if (normalCount > 0)
                result.AddRange(NormalEnemyList.GetRandomEnemies(normalCount));
            if (hardCount > 0)
                result.AddRange(HardEnemyList.GetRandomEnemies(hardCount));
            return result;
        }

    }
}