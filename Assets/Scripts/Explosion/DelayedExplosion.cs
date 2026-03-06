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

        private const float TimeExpiredThreshold = 0f;

        private bool _isExploding = false;
        private bool _isExploded = false;

        public float RadiusExplosion => _radiusExplosion;
        public bool IsExploded => _isExploded;

        public void Awake()
        {
            if (TryGetComponent(out SphereCollider collider))
            {
                collider.radius = _radiusActivation;
            }
        }

        public void Update()
        {
            if (_isExploding == true)
            {
                _countdownToExplosion -= Time.deltaTime;
            }

            if (_isExploded == true)
            {
                Destroy(gameObject);
            }

            if (_isExploded == false && _countdownToExplosion <= TimeExpiredThreshold)
            {
                Explode();
                _isExploded = true;
            }
        }

        public void OnTriggerEnter(Collider other)
        {
            if (_isExploding) return;

            _isExploding = true;
        }

        public void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _radiusExplosion);

            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, _radiusActivation);
        }

        private void Explode()
        {
            Collider[] targets = Physics.OverlapSphere(transform.position, _radiusExplosion);
            ExplosionData data = new ExplosionData(transform.position, _force, _radiusExplosion, _damage);

            foreach (Collider target in targets)
            {
                if (target.TryGetComponent(out IExplodable explodable) == false)
                    continue;

                explodable.OnExplode(data, target);
            }
        }
    }
}
