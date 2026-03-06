using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace LernUnityAdventure_m22_23
{
    public class DelayedExplosionView : MonoBehaviour
    {
        [SerializeField] private GameObject _particlePrefab;
        [SerializeField] private DelayedExplosion _delayedExplosion;

        private const float RatioSpeedToTime = 10f;

        public void Update()
        {
            if (_delayedExplosion.IsExploded)
            {
                PlayExplosionVfx();
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
    }
}
