using UnityEngine;

namespace BadJuja.Player.Visuals {
    public class Model : MonoBehaviour
    {
        public Transform ModelFiringPoint;

        public Transform GetFiringPoint() => ModelFiringPoint;
        public Transform GetModelTransform() => transform;

        public static Model InstantiateModel(GameObject model, Transform parent)
        {
            GameObject go = Instantiate(model, parent, false);
            return go.GetComponent<Model>();
        }
    }
}
