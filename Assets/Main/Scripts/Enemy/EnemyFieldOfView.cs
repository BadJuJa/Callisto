using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BadJuja.Enemy {
    public class EnemyFieldOfView : MonoBehaviour {

        [Range(0, 360)]
        [SerializeField] private float viewAngle = 20;

        [SerializeField] private LayerMask targetMask;
        [SerializeField] private LayerMask obstacleMask;

        [SerializeField] private float UpdateDelay = .2f;

        private float reachDistance;
        private List<Transform> visibleTargets = new List<Transform>();
        private Collider[] targetsInReachDistance;

        public bool PlayerInReachDistance { get; private set; } = false;

        void Start()
        {
            StartCoroutine("FindTargetsWithDelay", UpdateDelay);
        }

        public void SetReachDistance(float value)
        {
            reachDistance = value;
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
            visibleTargets.Clear();
            targetsInReachDistance = Physics.OverlapSphere(transform.position, reachDistance, targetMask);

            for (int i = 0; i < targetsInReachDistance.Length; i++)
            {
                Transform target = targetsInReachDistance[i].transform;
                Vector3 dirToTarget = (target.position - transform.position).normalized;
                if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
                {
                    float dstToTarget = Vector3.Distance(transform.position, target.position);

                    if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
                    {
                        visibleTargets.Add(target);
                    }
                }
            }

            PlayerInReachDistance = visibleTargets.Count > 0;
        }


        public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
        {
            if (!angleIsGlobal)
            {
                angleInDegrees += transform.eulerAngles.y;
            }
            return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
        }
    }
}