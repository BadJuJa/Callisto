using UnityEngine;

namespace BadJuja.Core {
    public static class Helpers {
        private static Matrix4x4 _isoMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, -90, 0));

        public static Vector3 ToRotation(this Vector3 input) => _isoMatrix.MultiplyPoint3x4(input);
        public static Vector3 ToZXVector3(this Vector2 vector) => new(vector.x, 0, vector.y);
    }
}