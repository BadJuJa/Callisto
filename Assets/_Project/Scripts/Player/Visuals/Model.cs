using UnityEngine;

namespace BadJuja.Player.Visuals {
    public class Model : MonoBehaviour
    {
        public Transform ModelFiringPoint;

        public Transform GetFiringPoint() => ModelFiringPoint;
        public Transform GetModelTransform() => transform;
    }
}
