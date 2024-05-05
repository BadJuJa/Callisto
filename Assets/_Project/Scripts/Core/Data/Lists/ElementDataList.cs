using System.Collections.Generic;
using UnityEngine;

namespace BadJuja.Core.Data.Lists
{
    [CreateAssetMenu(fileName = "New Element Data List", menuName = "Data/Element Data")]
    public class ElementDataList : ScriptableObject
    {
        public List<ElementData> Elements;
    }
}
