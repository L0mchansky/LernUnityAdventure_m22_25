using UnityEngine;
using UnityEngine.UIElements;

namespace LernUnityAdventure_m22_23
{
    public class DelayedExplosionView : MonoBehaviour
    {
        [SerializeField] private GameObject _particlePrefab;
        [SerializeField] private DelayedExplosion _delayedExplosion;

        [SerializeField] private GameObject _mineExplosionPrefub;
        [SerializeField] private GameObject _minePreExplosionPrefub;
        private GameObject _currentPreExplosion;

        private const float RatioSpeedToTime = 10f;

        public void Update()
        {
            if (_delayedExplosion.IsExplodes && _currentPreExplosion == null)
            {
                PlayPreExplosionSfx();
            }

            if (_delayedExplosion.IsExploded)
            {
                PlayExplosionVfx();
                StopPreExplosionSfx();
                PlayExplosionSfx();
            }
        }

        private void PlayExplosionVfx()
        {
            float radiusExplosion = _delayedExplosion.RadiusExplosion;

            GameObject particleSystemObj = Instantiate(_particlePrefab, transform.position, Quaternion.identity);
            ParticleSystem particleSystem = particleSystemObj.GetComponent<ParticleSystem>();

            float lifeTime = Mathf.Sqrt(radiusExplosion / RatioSpeedToTime);
            float speed = Mathf.Sqrt(radiusExplosion * RatioSpeedToTime);

            particleSystem.startLifetime = lifeTime;
            particleSystem.startSpeed = speed;

            particleSystemObj.SetActive(true);
        }

        private void PlayPreExplosionSfx()
        {
            _currentPreExplosion = PlayClipAtPoint(_minePreExplosionPrefub, _delayedExplosion.transform.position);
        }

        private void StopPreExplosionSfx()
        {
            _currentPreExplosion.GetComponent<AudioSource>().Stop();
            Destroy(_currentPreExplosion);
        }

        private void PlayExplosionSfx()
        {
            Instantiate(_mineExplosionPrefub, _delayedExplosion.transform.position, Quaternion.identity);
        }

        private GameObject PlayClipAtPoint(GameObject audioSourcePrefab, Vector3 position)
        {
            return Instantiate(audioSourcePrefab, position, Quaternion.identity);
        }
    }
}
