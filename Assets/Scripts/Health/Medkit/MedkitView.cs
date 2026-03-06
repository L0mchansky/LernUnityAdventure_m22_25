using System.Collections;
using UnityEngine;

namespace LernUnityAdventure_m24_25
{
    public class MedkitView
    {
        private Medkit _medkit;
        private GameObject _particlePrefab;

        public MedkitView(Medkit medkit, MonoBehaviour runner, GameObject particlePrefab)
        {
            _medkit = medkit;
            _particlePrefab = particlePrefab;
            runner.StartCoroutine(Run());
        }

        private IEnumerator Run() {
            yield return new WaitUntil(() =>_medkit.IsUse);

            GameObject particleSystemObj = Object.Instantiate(_particlePrefab, _medkit.transform.position, Quaternion.identity);

            Object.Destroy(_medkit.gameObject);
        }
    }
}