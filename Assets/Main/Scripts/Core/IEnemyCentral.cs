using UnityEngine;

namespace BadJuja.Core {
    public interface IEnemyCentral {
        public bool PlayerInReachDistance { get; }
        public bool IsInitialized { get; }
        public Transform PlayerTransform { get; }
    }
}