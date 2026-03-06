using UnityEngine;
using LernUnityAdventure_m24_25;

namespace LernUnityAdventure_m22_23
{
    public class Character : MonoBehaviour, IDamageable, IExplodable, IHasHealth, IHealable
    {
        [SerializeField] private float _maxHealth;
        [SerializeField] private CharacterView _characterView;
        [SerializeField] private NavMeshAgent _navMeshAgent;

        private ComponentHealth _componentHealth;
        private bool _isWalking;
        private const float VelocityMagnitudeThreshold = 0.05f;

        public bool IsWalking => _isWalking;
        public Vector3 Destination => _navMeshAgent.destination;
        public Vector3 Velocity => _navMeshAgent.velocity;
        public float CurrentHealth => _health.CurrentHealth;
        public float PercentageHealth => _health.PercentageHealth;
        public bool IsLife => _health.IsLife;

        public void Awake()
        {
            _componentHealth = new ComponentHealth(_maxHealth);
        }

        public void Update()
        {
            OnWalking();
        }

        public void SetDestination(Vector3 value)
        {
            _navMeshAgent.destination = value;
        }

        public void SetVelocity(Vector3 value)
        {
            _navMeshAgent.velocity = value;
        }

        public void TakeDamage(float damage)
        {
            float newHealth = _health.CurrentHealth - damage;
            _health.SetHealth(newHealth);

            _characterView.PlayTakeDamage();
        }

        public void OnExplode(ExplosionData data)
        {
            TakeDamage(data.Damage);
        }
        private void OnWalking()
        {
            if (Velocity.magnitude >= VelocityMagnitudeThreshold)
            {
                _isWalking = true;
            }
            else
            {
                _isWalking = false;
            }
        }

        public void OnHealing(float healingValue, ComponentHealth componentHealth)
        {
            float newHealth = componentHealth.CurrentHealth + healingValue;
            componentHealth.SetHealth(newHealth);
        }
    }
}
