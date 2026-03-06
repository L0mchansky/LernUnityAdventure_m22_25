using LernUnityAdventure_m22_23;
using System.Collections;
using UnityEngine;

namespace LernUnityAdventure_m24_25
{
    public class CoroutineExplosionTrigger : IExplosionTriggerStrategy
    {
        private DelayedExplosion _explosion;
        private MonoBehaviour _runner;
        private float _time;
        private Coroutine _runCoroutine;

        public CoroutineExplosionTrigger(MonoBehaviour runner, float time)
        {
            _runner = runner;
            _time = time;
        }

        public void Initialize(DelayedExplosion explosion)
        {
            _explosion = explosion;
        }

        public void Tick(float dt)
        {
            if (_explosion.IsExplodes == false) return;

            if (_runCoroutine == null)
            {
                _runCoroutine = _runner.StartCoroutine(Run());
            }
        }

        private IEnumerator Run()
        {
            yield return new WaitForSeconds(_time);
            _explosion.Explode();
            _runCoroutine = null;
        }
    }
}
