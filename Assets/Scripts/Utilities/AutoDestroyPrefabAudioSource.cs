using UnityEngine;

namespace LernUnityAdventure_m24_25
{
    [RequireComponent(typeof(AudioSource))]
    public class AutoDestroyPrefabAudioSource: MonoBehaviour
    {
        private AudioSource _audioSource;

        public void Awake()
        {
            if (TryGetComponent(out AudioSource audioSource))
            {
                _audioSource = audioSource;
                _audioSource.Play();
                Destroy(gameObject, _audioSource.clip.length * ((Time.timeScale < 0.01f) ? 0.01f : Time.timeScale));
            }
            else
            {
                Debug.LogError("[AudioSourceToPoint] у создаваемого компонента отсутствует AudioSource");
                Destroy(gameObject);
            }
        }
    }
}