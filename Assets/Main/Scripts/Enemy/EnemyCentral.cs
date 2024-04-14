using BadJuja.Core;
using System.Collections;
using UnityEngine;

namespace BadJuja.Enemy {
    public class EnemyCentral : MonoBehaviour, IDamagable, IEnemyCentral, IEnemyInit {

        [Header("References")]
        private EnemyData _data;
        public EnemyHealthBar healthBar;

        [Header("General")]
        private float _maxHealth;
        private float _health;

        private bool _isInitialized = false;

        public bool PlayerInReachDistance => _enemyFieldOfView.PlayerInReachDistance;
        public bool IsInitialized { get => _isInitialized; private set => _isInitialized = value; }
        public EnemyData EnemyData { get => _data; private set => _data = value; }
        public Transform PlayerTransform { get; private set; }
        private EnemyFieldOfView _enemyFieldOfView;
        private EnemyMovement _enemyMovement;
        private EnemyAttack _enemyAttack;
        public float HealthPercentage => _health / _maxHealth;

        public void Initialize(EnemyData data)
        {
            _enemyFieldOfView = GetComponent<EnemyFieldOfView>();
            _enemyMovement = GetComponent<EnemyMovement>();
            _enemyAttack = GetComponent<EnemyAttack>();

            _data = data;

            _maxHealth = _data.Health;
            _health = _maxHealth;

            _enemyFieldOfView.SetReachDistance(_data.AttackRange);
            _enemyMovement.SetSpeed(_data.MovementSpeed);
            _enemyAttack.Initialize(_data);

            Instantiate(original: _data.Prefab, parent: gameObject.transform, rotation: Quaternion.identity, position: transform.position);

            IsInitialized = true;

            StartCoroutine(FindPlayer());
        }

        public void TakeDamage(float value)
        {
            if (_health > value)
            {
                _health -= value;
            }
            else
            {
                _health = 0;
                Die();

            }
            healthBar.UpdateHealth(_health / _maxHealth);
        }

        private void Die()
        {
            GlobalEvents.Send_OnEnemyDied(EnemyData.ExperienceReward);
            Destroy(gameObject);
        }

        private IEnumerator FindPlayer()
        {
            while (PlayerTransform == null)
            {
                PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
                yield return new WaitForSeconds(.5f);
            }
        }
    }
}