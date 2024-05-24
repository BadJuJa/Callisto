using System.Collections.Generic;
using UnityEngine;

namespace BadJuja.Core.Data.Lists {
    [CreateAssetMenu(fileName = "New Enemy Data List", menuName = "Data/Enemy/Data List")]
    public class EnemyList : ScriptableObject {
        public List<EnemyData> EnemyDatas = new();

        public EnemyData[] GetRandomEnemies(int count)
        {
            EnemyData[] result = new EnemyData[count];
            for (int i = 0; i <  count; i++)
            {
                result[i] = EnemyDatas[Random.Range(0, EnemyDatas.Count)];
            }
            return result;
        }
    }
}