using LernUnityAdventure_m22_23;
using System.Collections.Generic;
using UnityEngine;

namespace LernUnityAdventure_m24_25
{
    public class AudioPlayer: MonoBehaviour
    {
        [SerializeField] private AudioSource _mainMelody;
        [SerializeField] private AudioSource _characterWalk;
        [SerializeField] private AudioSource _mineExplosion;
        [SerializeField] private GameObject _minePreExplosionPrefub;
        [SerializeField] private AudioSource _medkitUse;

        [SerializeField] private MedkitSpawner _medkitSpawner;
        [SerializeField] private Character _character;
        
        private List<DelayedExplosion> _delayedExplosions;
        private List<DelayedExplosion> _delayedExplodes;

        //TODO: Живет тут
        private Dictionary<DelayedExplosion, GameObject> _playMinePreExplosionAudio;

        private void Update()
        {
            PlayCharacterAudio();
            PlayMedKitsAudio();
            //PlayDelayedEpxlosionAudio();
            //ControlDelayedExplosionAudio();
        }

        private void PlayCharacterAudio()
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

        private void PlayMedKitsAudio()
        {
            foreach (var medkit in _medkitSpawner.Medkits)
            {
                if (medkit.IsUse)
                {
                    AudioSource.PlayClipAtPoint(_medkitUse.clip, medkit.transform.position, _medkitUse.volume);
                }   
            }
        }

        private void PlayDelayedEpxlosionAudio()
        {
            foreach (var delayedExplosion in _delayedExplosions)
            {
                if (delayedExplosion.IsExplodes)
                {
                    GameObject minePreExplosion = PlayClipAtPoint(_minePreExplosionPrefub, delayedExplosion.transform.position);
                    _delayedExplosions.Remove(delayedExplosion);
                    _delayedExplodes.Add(delayedExplosion);
                    _playMinePreExplosionAudio.Add(delayedExplosion, minePreExplosion);
                }
            }
        }

        private void ControlDelayedExplosionAudio()
        {
            foreach (var delayedExplodes in _delayedExplodes)
            {
                if (delayedExplodes.IsExplodes == false)
                {
                    if (_playMinePreExplosionAudio.TryGetValue(delayedExplodes, out GameObject minePreExplosion))
                    {
                        _delayedExplodes.Remove(delayedExplodes);
                        _playMinePreExplosionAudio.Remove(delayedExplodes);
                        Destroy(minePreExplosion);
                        AudioSource.PlayClipAtPoint(_mineExplosion.clip, delayedExplodes.transform.position, _mineExplosion.volume);
                    }
                }
            }
        }

        private GameObject PlayClipAtPoint(GameObject audioSourcePrefab, Vector3 position)
        {
            GameObject audioSourceGameobject = Instantiate(audioSourcePrefab, position, Quaternion.identity);
            AudioSource audioSource = gameObject.GetComponent<AudioSource>();
            audioSource.Play();
            return audioSourceGameobject;
        }
    }
}
