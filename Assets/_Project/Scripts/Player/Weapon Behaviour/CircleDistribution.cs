using BadJuja.Core;
using System.Collections.Generic;
using UnityEngine;

namespace BadJuja.Player {
    public class CircleDistribution : MonoBehaviour, IWeaponBehaviour {

        public int MaxNumberOfObjects = 5;
        public float Radius = .5f;
        public float Speed = 1f;

        private GameObject ObjectToDistribute;
        private int _currentNumberOfObjects = 0;
        private GameObject[] distributedObjects;
        private CircleMovement[] circleMovementComponents;

        public void Init(dynamic Args)
        {
            Dictionary<string, dynamic> _args;
            try
            {
                _args = Args;
            }
            catch (System.Exception e)
            {
                print(e);
                return;
            }

            if (_args.TryGetValue("Prefab", out dynamic prefab))
                ObjectToDistribute = prefab;
            if (_args.TryGetValue("Radius", out dynamic radius))
                Radius = radius;

            distributedObjects = new GameObject[MaxNumberOfObjects];
            circleMovementComponents = new CircleMovement[MaxNumberOfObjects];

            for (int i = 0; i < MaxNumberOfObjects; i++)
            {
                distributedObjects[i] = Instantiate(ObjectToDistribute, transform.position, Quaternion.identity, transform);
                if (!distributedObjects[i].TryGetComponent(out circleMovementComponents[i]))
                {
                    circleMovementComponents[i] = distributedObjects[i].AddComponent<CircleMovement>();
                }
                circleMovementComponents[i].Init(transform, Speed, Radius);
                distributedObjects[i].SetActive(false);
            }

            ActivateNextObject();
            RedistributeObjects();
        }

        public void ActivateNextObject()
        {
            for (int i = 0; i < MaxNumberOfObjects; i++)
            {
                if (!distributedObjects[i].activeSelf)
                {
                    distributedObjects[i].SetActive(true);
                    _currentNumberOfObjects++;
                    RedistributeObjects();
                    break;
                }
            }
        }

        private void RedistributeObjects()
        {
            for (int i = 0; i < _currentNumberOfObjects; i++)
            {
                circleMovementComponents[i].AdjustAngle(i * GetAngleBetweenObjects(_currentNumberOfObjects));
            }
        }

        private float GetAngleBetweenObjects(int numberOfObjects)
        {
            return Mathf.PI * 2 / numberOfObjects;
        }

        public void Upgrade(dynamic Args)
        {
            Dictionary<string, dynamic> _args;
            try
            {
                _args = Args;
            }
            catch (System.Exception e)
            {
                print(e);
                return;
            }
            
            if (_args.TryGetValue("ObjectCount", out _))
            {
                ActivateNextObject();
            }
            if (_args.TryGetValue("Speed", out dynamic value))
            {
                foreach (var component in circleMovementComponents)
                {
                    component.AdjustSpeed(value);
                }
            }
        }
    }
}