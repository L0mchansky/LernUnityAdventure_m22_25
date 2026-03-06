using System.Collections;
using UnityEngine;

namespace LernUnityAdventure_m24_25
{
    public class MedkitView
    {
        private Medkit _medkit;
        private GameObject _particlePrefab;
        private AudioSource _medkitUseAudio;

        public MedkitView(Medkit medkit, MonoBehaviour runner, GameObject particlePrefab, AudioSource medkitUseAudio)
        {
            _medkit = medkit;
            _particlePrefab = particlePrefab;
            _medkitUseAudio = medkitUseAudio;
            runner.StartCoroutine(Run());
        }

        private IEnumerator Run() {
            yield return new WaitUntil(() =>_medkit.IsUse);

            GameObject particleSystemObj = Object.Instantiate(_particlePrefab, _medkit.transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(_medkitUseAudio.clip, _medkit.transform.position, _medkitUseAudio.volume);
        }
    }
}