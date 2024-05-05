using UnityEngine;

namespace BadJuja.Player
{
    public class PlayerModel : MonoBehaviour
    {
        public Transform ModelFiringPoint;

        public Transform GetFiringPoint() => ModelFiringPoint;
        public Transform GetModelTransform() => transform;
    }
}
