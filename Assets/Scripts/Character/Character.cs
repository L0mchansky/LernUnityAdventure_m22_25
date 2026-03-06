using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TextCore.Text;

namespace LernUnityAdventure_m22_23
{
    public class Character : MonoBehaviour, IDamageable, IExplodable, IHasHealth
    {
        [SerializeField] private float _maxHealth;
        [SerializeField] private CharacterView _characterView;
        [SerializeField] private NavMeshAgent _navMeshAgent;

        private ComponentHealth _health;
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
            _health = new ComponentHealth(_maxHealth);
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

        public void OnExplode(ExplosionData data, Collider collider)
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
    }
}
