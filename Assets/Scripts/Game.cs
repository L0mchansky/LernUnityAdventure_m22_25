using UnityEngine;

namespace LernUnityAdventure_m22_23
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private Character _character;

        [SerializeField] private float _speed;
        [SerializeField] private float _angularSpeed;
        [SerializeField] private float _acceleration;

        [SerializeField] private float _patrolRadius;
        [SerializeField] private float _retryDelayAfterFail;
        [SerializeField] private float _idleTimeToPatrol;
        [SerializeField] private float _arrivalThreshold;

        private const float DestinationChangeThreshold = 0.3f;

        private float _idleTimer;
        private Vector3 _lastDestination;
        private bool _isPatrolActive;

        private NavMeshCharacterController _navmeshCharacterController;
        private BoredomPatrolController _boredomPatrolController;

        private void Awake()
        {

            _navmeshCharacterController =
                new NavMeshCharacterController(
                    _character,
                    _speed,
                    _angularSpeed,
                    _acceleration
                    );

            _boredomPatrolController =
                new BoredomPatrolController(        
                    _character,
                    _patrolRadius,
                    _arrivalThreshold,
                    _retryDelayAfterFail,
                    _speed,
                    _angularSpeed,
                    _acceleration);

            _navmeshCharacterController.Enable();
            _boredomPatrolController.Disable();

            _lastDestination = _character.Destination;
        }

        public void Update()
        {
            _navmeshCharacterController.Update(Time.deltaTime);
            _boredomPatrolController.Update(Time.deltaTime);

            HandleControllerSwitching(Time.deltaTime);
        }

        private void HandleControllerSwitching(float deltaTime)
        {
            Vector3 currentDestination = _character.Destination;

            if ((currentDestination - _lastDestination).magnitude > DestinationChangeThreshold)
            {
                _lastDestination = currentDestination;

                if (_isPatrolActive)
                {
                    _boredomPatrolController.Disable();
                    _isPatrolActive = false;
                }

                _idleTimer = 0;
                return;
            }

            bool reachedDestination = Vector3.Distance(_character.transform.position, currentDestination) <= _arrivalThreshold;

            if (!_isPatrolActive && reachedDestination && _character.IsWalking == false)
            {
                _idleTimer += deltaTime;

                if (_idleTimer >= _idleTimeToPatrol)
                {
                    _boredomPatrolController.Enable();
                    _isPatrolActive = true;
                    _idleTimer = 0;
                }
            }
            else
            {
                _idleTimer = 0;
            }
        }
    }
}
