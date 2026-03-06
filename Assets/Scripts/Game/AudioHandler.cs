using System;
using UnityEngine.Audio;

namespace LernUnityAdventure_m24_25
{
    public class AudioHandler
    {
        private const float OffVolumeValue = -80;
        private const float OnVolumeValue = 0;

        private const string MainMelodyKey = "MainMelodyVolume";
        private const string EffectsVolumeKey = "EffectsVolume";

        private readonly AudioMixer _audioMixer;
        public AudioHandler(AudioMixer audioMixer)
        {
            _audioMixer = audioMixer;
        }

        public bool IsMusicOn() => IsVolumeOn(MainMelodyKey);
        public bool IsEffectsOn() => IsVolumeOn(EffectsVolumeKey);

        public void OffMusic() => OffVolume(MainMelodyKey);
        public void OnMusic() => OnVolume(MainMelodyKey);

        public void OffEffects() => OffVolume(EffectsVolumeKey);
        public void OnEffects() => OnVolume(EffectsVolumeKey);

        private bool IsVolumeOn(string key)
            => _audioMixer.GetFloat(key, out float volume) && Math.Abs(volume - OnVolumeValue) <= 0.01f;

        private void OnVolume(string key) => _audioMixer.SetFloat(key, OnVolumeValue);

        private void OffVolume(string key) => _audioMixer.SetFloat(key, OffVolumeValue);
    }
}
