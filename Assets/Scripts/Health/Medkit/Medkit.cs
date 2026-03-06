using LernUnityAdventure_m22_23;
using UnityEngine;

namespace LernUnityAdventure_m24_25
{
    public class Medkit: MonoBehaviour
    {
        private float _healingValue;
        private bool _isUse;

        public bool IsUse => _isUse;

        public void Initialize( float healingValue)
        {
            _healingValue = healingValue;
        }

        public void OnTriggerEnter(Collider collider)
        {
            if (collider.TryGetComponent(out IHealable componentHealable) == false) return;
            if (collider.TryGetComponent(out Character character) == false) return;

            _isUse = true;
            componentHealable.OnHealing(_healingValue, character.ComponentHealth);
        }
    }
}
