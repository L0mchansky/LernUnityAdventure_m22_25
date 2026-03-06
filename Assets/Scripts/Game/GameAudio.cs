using UnityEngine;

namespace LernUnityAdventure_m24_25
{
    public class GameAudio: MonoBehaviour
    {
        [SerializeField] private AudioSource _mainMelody;

        public void Start()
        {
            _mainMelody.Play();
        }
    }
}
