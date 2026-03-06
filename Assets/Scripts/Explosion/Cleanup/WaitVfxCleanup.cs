using LernUnityAdventure_m22_23;
using UnityEngine;

namespace LernUnityAdventure_m24_25
{
    public class WaitVfxCleanup : IExplosionCleanupStrategy
    {
        private DelayedExplosion _explosion;

        public void Initialize(DelayedExplosion explosion)
        {
            _explosion = explosion;
        }

        public void Tick(float dt)
        {
            if (_explosion.HasVfxPlayed) 
                Object.Destroy(_explosion.gameObject);
        }
    }
}
