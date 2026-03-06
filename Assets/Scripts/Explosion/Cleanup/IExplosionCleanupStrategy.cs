using LernUnityAdventure_m22_23;

namespace LernUnityAdventure_m24_25
{
    public interface IExplosionCleanupStrategy
    {
        void Initialize(DelayedExplosion explosion);
        void Tick(float deltaTime);
    }
}
