﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyFieldOfView : MonoBehaviour {

	public float viewRadius;
	[Range(0,360)]
	public float viewAngle = 20;

	public LayerMask targetMask;
	public LayerMask obstacleMask;

	public float UpdateDelay = .2f;

	[HideInInspector]
	public List<Transform> visibleTargets = new List<Transform>();

    private IEnemyCentral _enemyCentralInterface;

    private void Awake()
    {
        _enemyCentralInterface = GetComponent<IEnemyCentral>();
    }

    void Start() {
		StartCoroutine ("FindTargetsWithDelay", UpdateDelay);
		if (_enemyCentralInterface.EnemyData != null) viewRadius = _enemyCentralInterface.EnemyData.AttackRange;
	}


	IEnumerator FindTargetsWithDelay(float delay) {
		while (true) {
			yield return new WaitForSeconds (delay);
			FindVisibleTargets ();
		}
	}

	void FindVisibleTargets() {
		visibleTargets.Clear ();
		Collider[] targetsInViewRadius = Physics.OverlapSphere (transform.position, viewRadius, targetMask);

		for (int i = 0; i < targetsInViewRadius.Length; i++) {
			Transform target = targetsInViewRadius [i].transform;
			Vector3 dirToTarget = (target.position - transform.position).normalized;
			if (Vector3.Angle (transform.forward, dirToTarget) < viewAngle / 2) {
				float dstToTarget = Vector3.Distance (transform.position, target.position);

				if (!Physics.Raycast (transform.position, dirToTarget, dstToTarget, obstacleMask)) {
					visibleTargets.Add (target);
				}
			}
		}
		
        _enemyCentralInterface.PlayerInReachDistance = visibleTargets.Count > 0;
	}


	public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal) {
		if (!angleIsGlobal) {
			angleInDegrees += transform.eulerAngles.y;
		}
		return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad),0,Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
	}
}