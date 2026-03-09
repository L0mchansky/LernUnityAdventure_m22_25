using LernUnityAdventure_m24_25;
using System;
using UnityEngine;

namespace LernUnityAdventure_m22_23
{
    [RequireComponent(typeof(SphereCollider))]
    public class DelayedExplosion : MonoBehaviour
    {
        [SerializeField] private float _damage;
        [SerializeField] private float _force;
        [SerializeField] private float _radiusExplosion;
        [SerializeField] private float _radiusActivation;
        [SerializeField] private float _countdownToExplosion;

        [SerializeField] private ExplosionTriggerTypes _triggerType;

        private IExplosionTriggerStrategy _trigger;

        private bool _isExplodes = false;
        private bool _isExploded = false;

        public float RadiusExplosion => _radiusExplosion;
        public bool IsExploded => _isExploded;
        public bool IsExplodes => _isExplodes;

        public void Awake()
        {
            _trigger = CreateTrigger();
            _trigger.Initialize(this);

            if (TryGetComponent(out SphereCollider collider))
            {
                collider.radius = _radiusActivation;
            }
        }

        private IExplosionTriggerStrategy CreateTrigger()
        {
            switch (_triggerType)
            {
                case ExplosionTriggerTypes.Timer:
                    return new TimerExplosionTrigger(_countdownToExplosion);

                case ExplosionTriggerTypes.Coroutine:
                    return new CoroutineExplosionTrigger(this, _countdownToExplosion);

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void Update()
        {
            if (_isExploded == true)
            {
                Destroy(gameObject);
            }
            else
            {
                _trigger.Tick(Time.deltaTime);
            }
        }

        public void OnTriggerEnter(Collider other)
        {
            if (_isExplodes) return;

            _isExplodes = true;
        }

        public void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _radiusExplosion);

            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, _radiusActivation);
        }

        public void Explode()
        {
            _isExplodes = false;

            Collider[] targets = Physics.OverlapSphere(transform.position, _radiusExplosion);
            ExplosionData data = new ExplosionData(transform.position, _force, _radiusExplosion, _damage);

            foreach (Collider target in targets)
            {
                if (target.TryGetComponent(out IExplodable explodable) == false)
                    continue;

                explodable.OnExplode(data);
            }

            _isExploded = true;
        }

        public float GetTimeToExplosion()
        {
            return _trigger.GetTimeToExplosion();
        }
    }
}
