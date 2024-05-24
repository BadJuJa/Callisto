using BadJuja.Core;
using System.Collections.Generic;
using UnityEngine;

namespace BadJuja.Player.Weapons.Behaviour {
    public class CircleDistribution : IWeaponBehaviour {
         
        private int _maxNumberOfObjects;
        private float _radius = .5f;
        private float _speed = 1f;

        private int _currentNumberOfObjects = 0;

        private GameObject[] _distributedObjects;
        private CircleMovement[] _circleMovementComponents;

        public CircleDistribution(Transform weaponTransform, GameObject[] distributedObjects, float radius)
        {
            _distributedObjects = distributedObjects;
            _maxNumberOfObjects = _distributedObjects.Length;

            _radius = radius;

            _circleMovementComponents = new CircleMovement[_maxNumberOfObjects];

            for (int i = 0; i < _distributedObjects.Length; i++)
            {
                if (!_distributedObjects[i].TryGetComponent(out _circleMovementComponents[i]))
                {
                    _circleMovementComponents[i] = _distributedObjects[i].AddComponent<CircleMovement>();
                }
                _circleMovementComponents[i].Init(weaponTransform, _speed, _radius);
                _distributedObjects[i].SetActive(false);
            }

            ActivateNextObject();
        }

        public void ActivateNextObject()
        {
            for (int i = 0; i < _maxNumberOfObjects; i++)
            {
                if (!_distributedObjects[i].activeSelf)
                {
                    _distributedObjects[i].SetActive(true);
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
                _circleMovementComponents[i].AdjustAngle(i * (Mathf.PI * 2 / _currentNumberOfObjects));
            }
        }

        public void Upgrade(Dictionary<string, float> args)
        {
            if (args.ContainsKey("Speed"))
            {
                for(int i = 0; i < _circleMovementComponents.Length; i++)
                {
                    _circleMovementComponents[i].IncreaseSpeed(args["Speed"]);
                }
            }

            if (args.ContainsKey("Count"))
                ActivateNextObject();
        }
    }
}