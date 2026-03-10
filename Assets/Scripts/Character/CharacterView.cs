using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

namespace LernUnityAdventure_m22_23
{
    public class CharacterView : MonoBehaviour
    {
        private static readonly int _isWalkingKey = Animator.StringToHash("IsWalking");
        private static readonly int _isJumpProcessKey = Animator.StringToHash("InJumpProcess");
        private static readonly int _takeDamageKey = Animator.StringToHash("TakeDamage");
        private static readonly int _dieKey = Animator.StringToHash("Die");
        private static readonly string _injuredLayerName = "Injured Layer";
        private static readonly string _shaderDissolveKey = "_Edge";

        private const float InjuredHealthPercentThreshold = 30f;

        [SerializeField] private Character _character;
        [SerializeField] private AudioSource _characterWalk;

        [SerializeField] private Animator _animator;
        [SerializeField] private AnimationCurve _jumpCurve;

        [SerializeField] private float _timeToDissolved;
        [SerializeField] private SkinnedMeshRenderer[] _renderers;

        private bool _isPlayedInjure = false;
        private bool _isPlayedDie = false;

        public void Awake()
        {
            _character.SetAnimationCurve(_jumpCurve);
            _renderers = GetComponentsInChildren<SkinnedMeshRenderer>();
        }

        public void Update()
        {
            PlayAnimations();
            PlayAudio();
        }

        public void PlayTakeDamage()
        {
            _animator.SetTrigger(_takeDamageKey);
        }

        private void PlayAnimations()
        {
            if (_character.IsWalking)
            {
                StartWalking();
            }
            else
            {
                StopWalking();
            }

            if (_character.IsJumping)
            {
                StartJumping();
            }
            else
            {
                StopJumping();
            }

            if (_isPlayedInjure == false && _character.PercentageHealth <= InjuredHealthPercentThreshold)
            {
                PlayInjured();
            }

            if (_isPlayedInjure == true && _character.PercentageHealth >= InjuredHealthPercentThreshold)
            {
                StopInjured();
            }

            if (_isPlayedDie == false && _character.IsLife == false)
            {
                PlayDie();
            }
        }
        private void PlayAudio()
        {
            if (_character.IsWalking)
            {
                if (_characterWalk.isPlaying == false)
                    _characterWalk.Play();
            }
            else
            {
                if (_characterWalk.isPlaying)
                    _characterWalk.Stop();
            }
        }

        private void StartWalking()
        {
            _animator.SetBool(_isWalkingKey, true);
        }

        private void StopWalking()
        {
            _animator.SetBool(_isWalkingKey, false);
        }

        private void StartJumping()
        {
            _animator.SetBool(_isJumpProcessKey, true);
        }

        private void StopJumping()
        {
            _animator.SetBool(_isJumpProcessKey, false);
        }

        private void PlayInjured()
        {
            _isPlayedInjure = true;
            ChangeInjured(1);
        }

        private void StopInjured()
        {
            _isPlayedInjure = false;
            ChangeInjured(0);
        }

        private void ChangeInjured(int layerWeight)
        {
            int injuredLayerIndex = _animator.GetLayerIndex(_injuredLayerName);
            _animator.SetLayerWeight(injuredLayerIndex, layerWeight);
        }

        private void PlayDie()
        {
            _isPlayedDie = true;
            _animator.SetTrigger(_dieKey);
            StartCoroutine(Dissolve());
        }

        private IEnumerator Dissolve()
        {
            float elapsedTime = 0f;

            while (elapsedTime < _timeToDissolved)
            {
                elapsedTime += Time.deltaTime;
                float edge = elapsedTime / _timeToDissolved;

                foreach (var renderer in _renderers)
                {
                    renderer.material.SetFloat(_shaderDissolveKey, edge);
                }

                yield return null;
            }

            Destroy(_character.gameObject);
        }
    }
}
