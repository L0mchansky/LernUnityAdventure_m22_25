using System.Collections;
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace LernUnityAdventure_m22_23
{
    public class DelayedExplosionView : MonoBehaviour
    {

        private const string scaleKey = "_Scale";
        private const string colorOverlayKey = "_ColorOverlay";

        [SerializeField] private GameObject _particlePrefab;
        [SerializeField] private DelayedExplosion _delayedExplosion;

        [SerializeField] private GameObject _mineExplosionPrefub;
        [SerializeField] private GameObject _minePreExplosionPrefub;
        [SerializeField] private float _pulseSpeed;
        [SerializeField] private float _minScale;
        [SerializeField] private float _maxScale;

        private GameObject _currentPreExplosion;

        private const float RatioSpeedToTime = 10f;

        private Material _material;

        public void Awake()
        {
            _material = GetComponent<MeshRenderer>().material;
        }

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
            StartCoroutine(PlayPreExplosionShader());
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

        private IEnumerator PlayPreExplosionShader()
        {
            float timeToExplosion = _delayedExplosion.GetTimeToExplosion();
            float elapsedTime = 0f;

            while (elapsedTime < timeToExplosion)
            {
                elapsedTime += Time.deltaTime;
                float normalizedTime = elapsedTime / timeToExplosion;

                float colorOverlay = (Mathf.Sin(Time.time * _pulseSpeed * Mathf.PI) + 1f) / 2f;

                float scale = Mathf.Lerp(_minScale, _maxScale, colorOverlay);

                _material.SetFloat(scaleKey, scale);
                _material.SetFloat(colorOverlayKey, colorOverlay);

                yield return null;
            }
        }
    }
}
