using BadJuja.Core;
using UnityEngine;

namespace BadJuja.Player {

    public class CircleMovement : MonoBehaviour {

        public bool AccountPlayer = false;

        private Transform _parent;
        private IWeapon _parentWeapon;
        private float _speed = 2.0f;
        private float _radius = 2.0f;

        private float _angle = 0;

        private bool _initiated = false;
        public void Init(Transform parent, float speed, float radius)
        {
            _parent = parent;
            _parentWeapon = parent.GetComponent<IWeapon>();
            _radius = radius;

            transform.rotation = Quaternion.Euler(-90, 0, 0);
            AdjustSpeed(speed);

            _initiated = true;
        }

        public void AdjustAngle(float angle)
        {
            _angle = angle;
        }

        public void AdjustSpeed(float speed)
        {
            _speed = speed;
        }

        void Update()
        {
            if (!_initiated) return;

            transform.position = CalculateNewPosition();

            Vector3 relativePos = _parent.position - transform.position;
            transform.rotation = Quaternion.LookRotation(-relativePos);
            UpdateAngle();
        }

        private Vector3 CalculateNewPosition()
        {
            return _parent.position + new Vector3(Mathf.Cos(_angle) * _radius, 0, Mathf.Sin(_angle) * _radius);
        }

        private void UpdateAngle()
        {
            _angle += Time.deltaTime * _speed;

            if (_angle > 360f)
            {
                _angle -= 360f;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!_initiated) return;

            if (!other.CompareTag("Enemy")) return;

            if (other.TryGetComponent(out IDamagable damagable))
            {
                _parentWeapon.DealDamage(damagable);
            }
        }
    }
}