using System.Collections;
using UnityEngine;

namespace LernUnityAdventure_m24_25
{
    public class MedkitView
    {
        private Medkit _medkit;
        private GameObject _particlePrefab;
        private GameObject _medkitUseAudioPrefab;

        public MedkitView(Medkit medkit, MonoBehaviour runner, GameObject particlePrefab, GameObject medkitUseAudioPrefab)
        {
            _medkit = medkit;
            _particlePrefab = particlePrefab;
            _medkitUseAudioPrefab = medkitUseAudioPrefab;
            runner.StartCoroutine(Run());
        }

        private IEnumerator Run() {
            yield return new WaitUntil(() =>_medkit.IsUse);

            GameObject particleSystemObj = Object.Instantiate(_particlePrefab, _medkit.transform.position, Quaternion.identity);
            Object.Instantiate(_medkitUseAudioPrefab, _medkit.transform.position, Quaternion.identity);
        }
    }
}