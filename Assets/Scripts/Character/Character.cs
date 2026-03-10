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

        private ComponentHealth _componentHealth;
        private AgentJumper _agentJumper;

        private bool _isWalking;
        private const float VelocityMagnitudeThreshold = 0.05f;

        public bool IsWalking => _isWalking;
        public bool IsJumping => _agentJumper.InProcess;
        public Vector3 Destination => _navMeshAgent.destination;
        public Vector3 Velocity => _navMeshAgent.velocity;
        public float CurrentHealth => _componentHealth.CurrentHealth;
        public float PercentageHealth => _componentHealth.PercentageHealth;
        public bool IsLife => _componentHealth.IsLife;

        public void Awake()
        {
            _componentHealth = new ComponentHealth(_maxHealth);
            _agentJumper = new AgentJumper(_jumpSpeed, _navMeshAgent, this);
        }

        public void Update()
        {
            OnWalking();
            OnJumping();
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

        public void SetAnimationCurve(AnimationCurve yOffSetCurve)
        {
            _agentJumper?.SetAnimationCurve(yOffSetCurve);
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

        private void OnJumping()
        {
            if (InOnNavMeshLink(out OffMeshLinkData offMeshLinkData))
            {
                _agentJumper.Jump(offMeshLinkData);
            }
        }

        private bool InOnNavMeshLink(out OffMeshLinkData offMeshLinkData)
        {
            if (_navMeshAgent.isOnOffMeshLink)
            {
                offMeshLinkData = _navMeshAgent.currentOffMeshLinkData;
                return true;
            }

            offMeshLinkData = default(OffMeshLinkData);
            return false;
        }
    }
}
