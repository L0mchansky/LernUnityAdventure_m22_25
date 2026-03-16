using LernUnityAdventure_m23_24;
using LernUnityAdventure_m24_25;
using UnityEngine;
using UnityEngine.AI;

namespace LernUnityAdventure_m22_23
{
    public class Character : MonoBehaviour, IDamageable, IExplodable, IHasHealth, IHealable
    {
        [SerializeField] private float _maxHealth;
        [SerializeField] private float _jumpSpeed;
        [SerializeField] private CharacterView _characterView;
        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private AnimationCurve _jumpCurve;

        private ComponentHealth _componentHealth;
        private AgentJumper _agentJumper;

        private bool _isWalking;
        private const float VelocityMagnitudeThreshold = 0.05f;

        public bool IsWalking => _isWalking;
        public bool IsJumping => _agentJumper.InProcess;
        public AgentJumper AgentJumper => _agentJumper;
        public Vector3 Destination => _navMeshAgent.destination;
        public Vector3 Velocity => _navMeshAgent.velocity;
        public OffMeshLinkData CurrentOffMeshLinkData => _navMeshAgent.currentOffMeshLinkData;
        public bool IsOnOffMeshLink => _navMeshAgent.isOnOffMeshLink;
        public float CurrentHealth => _componentHealth.CurrentHealth;
        public float PercentageHealth => _componentHealth.PercentageHealth;
        public bool IsLife => _componentHealth.IsLife;

        public void Awake()
        {
            _componentHealth = new ComponentHealth(_maxHealth);
            _agentJumper = new AgentJumper(_jumpSpeed, _navMeshAgent, this);

            _agentJumper.SetAnimationCurve(_jumpCurve);
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
            float newHealth = CurrentHealth - damage;
            _componentHealth.SetHealth(newHealth);

            _characterView.PlayTakeDamage();
        }

        public void OnExplode(ExplosionData data)
        {
            TakeDamage(data.Damage);
        }

        public void OnHealing(float healingValue)
        {
            float newHealth = CurrentHealth + healingValue;
            _componentHealth.SetHealth(newHealth);
        }

        public bool CanMove()
        {
            if (IsLife == false)
                return false;

            if (IsJumping)
                return false;

            return true;
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
