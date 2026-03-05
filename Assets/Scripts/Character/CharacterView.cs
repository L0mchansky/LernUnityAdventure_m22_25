using UnityEngine;

namespace LernUnityAdventure_m22_23
{
    public class CharacterView : MonoBehaviour
    {
        private static readonly int _isWalkingKey = Animator.StringToHash("IsWalking");
        private static readonly int _takeDamageKey = Animator.StringToHash("TakeDamage");
        private static readonly int _dieKey = Animator.StringToHash("Die");
        private static readonly string _injuredLayerName = "Injured Layer";

        private const float InjuredHealthPercentThreshold = 30f;

        [SerializeField] private Character _character;

        private Animator _animator;
        private ComponentHealth _componentHealth;
        private bool _isPlayedInjure = false;
        private bool _isPlayedDie = false;

        public void Awake()
        {
            _animator = GetComponent<Animator>();
            _componentHealth = _character.ComponentHealth;
        }

        public void Update()
        {
            if (_character.IsWalking)
            {
                StartWalking();
            }
            else
            {
                StopWalking();
            }

            if (_isPlayedInjure == false && _componentHealth.PercentageHealth <= InjuredHealthPercentThreshold)
            {
                PlayInjured();
            }

            if (_isPlayedInjure == true && _componentHealth.PercentageHealth >= InjuredHealthPercentThreshold) 
            {
                StopInjured();
            }

            if (_isPlayedDie == false && _componentHealth.IsLife == false)
            {
                PlayDie();
            }
        }

        private void StopWalking()
        {
            _animator.SetBool(_isWalkingKey, false);
        }

        private void StartWalking()
        {
            _animator.SetBool(_isWalkingKey, true);
        }

        public void PlayTakeDamage()
        {
            _animator.SetTrigger(_takeDamageKey);
        }

        public void PlayInjured()
        {
            _isPlayedInjure = true;
            ChangeInjured(1);
        }

        public void StopInjured()
        {
            _isPlayedInjure = false;
            ChangeInjured(0);
        }

        private void ChangeInjured(int layerWeight)
        {
            int injuredLayerIndex = _animator.GetLayerIndex(_injuredLayerName);
            _animator.SetLayerWeight(injuredLayerIndex, layerWeight);
        }

        public void PlayDie()
        {
            _isPlayedDie = true;
            _animator.SetTrigger(_dieKey);
        }
    }
}
