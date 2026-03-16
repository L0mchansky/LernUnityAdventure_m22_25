using System.Collections;
using UnityEngine;

namespace LernUnityAdventure_m24_25
{
    public class MedkitView: MonoBehaviour
    {
        [SerializeField] private GameObject _particlePrefab;
        [SerializeField] private GameObject _medkitUseAudioPrefab;
        [SerializeField] private Medkit _medkit;

        public void Awake()
        {
            StartCoroutine(Run());
        }

        private IEnumerator Run() {
            yield return new WaitUntil(() =>_medkit.IsUse);

            GameObject particleSystemObj = Object.Instantiate(_particlePrefab, _medkit.transform.position, Quaternion.identity);
            Object.Instantiate(_medkitUseAudioPrefab, _medkit.transform.position, Quaternion.identity);
        }
    }
}