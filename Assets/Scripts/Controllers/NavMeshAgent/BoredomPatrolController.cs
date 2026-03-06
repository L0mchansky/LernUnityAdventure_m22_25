using UnityEngine;
using UnityEngine.AI;

namespace LernUnityAdventure_m22_23
{
    public class BoredomPatrolController : Controller
    {
        private const int MaxSampleAttempts = 30;

        private readonly Character _character;
        private readonly float _patrolRadius;
        private readonly float _arrivalThreshold;
        private readonly float _retryDelayAfterFail;

        private float _nextPatrolAttemptTime;

        public BoredomPatrolController(
            Character character,
            float patrolRadius,
            float arrivalThreshold,
            float retryDelayAfterFail,
            float speed,
            float angularSpeed,
            float acceleration)
        {
            _character = character;
            _patrolRadius = patrolRadius;
            _arrivalThreshold = arrivalThreshold;
            _retryDelayAfterFail = retryDelayAfterFail;

            _character.SetDestination(_character.transform.position);
        }

        protected override void UpdateLogic(float deltaTime)
        {
            if (!_character.IsLife)
                return;

            if (!ReachedDestination())
                return;

            TrySetNextPatrolDestination();
        }

        private bool ReachedDestination()
        {
            Vector3 destination = _character.Destination;
            float distance = Vector3.Distance(_character.transform.position, destination);
            return distance <= _arrivalThreshold;
        }

        private void TrySetNextPatrolDestination()
        {
            if (Time.time < _nextPatrolAttemptTime)
                return;

            Vector3? point = TryGetRandomPointOnNavMesh();

            if (point.HasValue)
            {
                _character.SetDestination(point.Value);
                _nextPatrolAttemptTime = 0f;
            }
            else
            {
                _nextPatrolAttemptTime = Time.time + _retryDelayAfterFail;
            }
        }

        private Vector3? TryGetRandomPointOnNavMesh()
        {
            Vector3 origin = _character.transform.position;

            for (int i = 0; i < MaxSampleAttempts; i++)
            {
                Vector2 circle = Random.insideUnitCircle * _patrolRadius;
                Vector3 sample = origin + new Vector3(circle.x, 0f, circle.y);

                if (NavMesh.SamplePosition(sample, out NavMeshHit hit, _patrolRadius, NavMesh.AllAreas))
                    return hit.position;
            }

            return null;
        }
    }
}