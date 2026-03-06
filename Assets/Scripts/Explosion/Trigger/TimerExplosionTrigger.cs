using LernUnityAdventure_m22_23;

namespace LernUnityAdventure_m24_25
{
    public class TimerExplosionTrigger : IExplosionTriggerStrategy
    {
        private float _time;
        private DelayedExplosion _explosion;


        public TimerExplosionTrigger(float time)
        {
            _time = time;
        }

        public void Initialize(DelayedExplosion explosion)
        {
            _explosion = explosion;
        }

        public void Tick(float deltaTime)
        {
            if (_explosion.IsExplodes == false) return;

            _time -= deltaTime;

            if (_time <= 0f)
                _explosion.Explode();
        }
    }
}
