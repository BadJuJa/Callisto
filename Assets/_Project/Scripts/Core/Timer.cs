using System;
using System.Collections;
using UnityEngine;

namespace BadJuja.Core
{
    public class Timer
    {
        private float _time;
        private float _remainingTime;

        private IEnumerator _countdown;

        private MonoBehaviour _context;

        public event Action<float> HasBeenUpdated;
        public event Action TimeIsOver;

        public Timer(MonoBehaviour context) => _context = context;

        public void Set(float time)
        {
            _time = time;
            _remainingTime = _time;
        }

        public void StartCountingTime()
        {
            _countdown = Countdown();
            _context.StartCoroutine(_countdown);
        }

        public void StopCountingTime()
        {
            if (_countdown != null )
                _context.StopCoroutine(_countdown);
        }

        private IEnumerator Countdown()
        {
            while (_remainingTime >= 0)
            {
                _remainingTime -= Time.deltaTime;

                HasBeenUpdated?.Invoke(_remainingTime / _time);
                yield return null;
            }

            TimeIsOver?.Invoke();
        }
    }
}
