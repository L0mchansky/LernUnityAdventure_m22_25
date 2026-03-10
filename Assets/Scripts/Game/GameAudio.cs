using UnityEngine;
using UnityEngine.Audio;

namespace LernUnityAdventure_m24_25
{
    public class GameAudio: MonoBehaviour
    {
        [SerializeField] private AudioSource _mainMelody;
        [SerializeField] private AudioMixer _audioMixer;

        private AudioHandler _audioHandler;

        public void Awake()
        {
            _audioHandler = new AudioHandler(_audioMixer);
        }

        public void Start()
        {
            _mainMelody.Play();
        }

        public void ToggleMusic()
        {
            if (_audioHandler.IsMusicOn())
            {
                _mainMelody.Stop();
                _audioHandler.OffMusic();
            }
            else
            {
                _mainMelody.Play();
                _audioHandler.OnMusic();
            }
        }

        public void ToggleEffects()
        {
            if (_audioHandler.IsEffectsOn())
            {
                _audioHandler.OffEffects();
            }
            else
            {
                _audioHandler.OnEffects();
            }
        }
    }
}
