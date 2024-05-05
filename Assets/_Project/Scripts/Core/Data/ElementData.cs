using UnityEngine;

namespace BadJuja.Core.Data
{
    [CreateAssetMenu(fileName = "New Element Data", menuName = "Data/Element")]
    public class ElementData : ScriptableObject
    {
        public WeaponElements Element;
        public Sprite Sprite;
        public Color Color;

    }
}
