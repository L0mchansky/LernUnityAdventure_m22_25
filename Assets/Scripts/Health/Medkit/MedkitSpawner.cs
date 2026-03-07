using LernUnityAdventure_m22_23;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LernUnityAdventure_m24_25
{
    public class MedkitSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _particlePrefab;
        [SerializeField] private GameObject _medkitUseAudioPrefab;

        [SerializeField] private Character _character;
        [SerializeField] private GameObject _medkitPrefab;
        [SerializeField] private float _spawnHeight = 1f;

        [SerializeField] private float _timeToSpawn = 3f;
        [SerializeField] private float _distanceToSpawn = 5f;
        [SerializeField] private float _radiusToSpawn = 1f;
        [SerializeField] private float _healingValue = 10f;

        private List<Medkit> _medkits = new();
        private Coroutine _runCoroutine;
        private bool _isActive = false;

        public List<Medkit> Medkits => _medkits;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                ToggleSpawner();
            }

            if (_medkits.Count > 0)
            {
                DestroyMedkit();
            }
        }

        private void DestroyMedkit()
        {

            for (int i = _medkits.Count - 1; i >= 0; i--)
            {
                var medkit = _medkits[i];

                if (medkit.IsUse)
                {
                    _medkits.RemoveAt(i);
                    Destroy(medkit.gameObject);
                }
            }
        }

        private void ToggleSpawner()
        {
            _isActive = _isActive != true;

            if (_isActive)
            {
                _runCoroutine = StartCoroutine(SpawnLoop());
            }
            else if (_runCoroutine != null)
            {
                StopCoroutine(_runCoroutine);
            }
                
        }

        private IEnumerator SpawnLoop()
        {
            while (true)
            {
                yield return new WaitForSeconds(_timeToSpawn);

                Medkit medkit = Instantiate(_medkitPrefab).GetComponent<Medkit>();
                medkit.Initialize(_healingValue);
                medkit.transform.position = MedkitSpawnPoint();

                MedkitView medkitView = new MedkitView(medkit, this, _particlePrefab, _medkitUseAudioPrefab);

                _medkits.Add(medkit);
            }
        }

        private Vector3 MedkitSpawnPoint()
        {
            Vector3 characterPosition = _character.transform.position;

            Vector2 randomPoint = Random.insideUnitCircle * _radiusToSpawn;
            Vector3 direction = new Vector3(randomPoint.x, 0f, randomPoint.y).normalized;

            Vector3 spawnPosition = characterPosition + direction * _distanceToSpawn;
            spawnPosition.y = _spawnHeight;

            return spawnPosition;
        }
    }
}