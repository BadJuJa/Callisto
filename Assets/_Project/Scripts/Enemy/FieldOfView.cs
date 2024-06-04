using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BadJuja.Enemy {
    public class FieldOfView {
        private Transform _ownerTransform;
        private MonoBehaviour _context;

        private float _viewAngle = 20;
        private float _updateDelay = .2f;
        private float _reachDistance;

        private LayerMask _targetMask;
        private LayerMask _obstacleMask;

        private List<Transform> _visibleTargets = new();
        private Collider[] _targetsInReachDistance;

        public bool PlayerInReachDistance { get; private set; } = false;

        public void Initiate(Transform transform, float reachDistance = 5f, float viewAngle = 60, MonoBehaviour context = null, float updateDelay = .2f)
        {
            _targetMask = LayerMask.GetMask("Player");
            _obstacleMask = LayerMask.GetMask("Walls");
            
            _ownerTransform = transform;
            
            if (context == null) _context = _ownerTransform.GetComponent<MonoBehaviour>();
            else _context = context;

            _reachDistance = reachDistance;
            _viewAngle = Mathf.Clamp(viewAngle, 0, 360);

            _context.StartCoroutine(FindTargetsWithDelay(_updateDelay));
        }

        private IEnumerator FindTargetsWithDelay(float delay)
        {
            while (true)
            {
                yield return new WaitForSeconds(delay);
                FindVisibleTargets();
            }
        }

        private void FindVisibleTargets()
        {
            _visibleTargets.Clear();
            _targetsInReachDistance = Physics.OverlapSphere(_ownerTransform.position, _reachDistance, _targetMask);

            for (int i = 0; i < _targetsInReachDistance.Length; i++)
            {
                Transform target = _targetsInReachDistance[i].transform;
                Vector3 dirToTarget = (target.position - _ownerTransform.position).normalized;
                if (Vector3.Angle(_ownerTransform.forward, dirToTarget) < _viewAngle / 2)
                {
                    float dstToTarget = Vector3.Distance(_ownerTransform.position, target.position);

                    if (!Physics.Raycast(_ownerTransform.position, dirToTarget, dstToTarget, _obstacleMask))
                    {
                        _visibleTargets.Add(target);
                    }
                }
            }

            PlayerInReachDistance = _visibleTargets.Count > 0;
        }
    }
}