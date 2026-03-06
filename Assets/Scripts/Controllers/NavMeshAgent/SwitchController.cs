using UnityEngine;
using UnityEngine.TextCore.Text;

namespace LernUnityAdventure_m22_23
{
    public class SwitchController : Controller
    {

        private float _idleTimer;
        private Vector3 _lastDestination;

        private Character _character;
        private float _patrolRadius;
        private float _retryDelayAfterFail;
        private float _idleTimeToPatrol;
        private float _arrivalThreshold;

        private const float DestinationChangeThreshold = 0.3f;

        private Controller _currentController;

        private NavMeshCharacterController _navMeshCharacterController;
        private BoredomPatrolController _boredomPatrolController;

        public SwitchController(Character character, float patrolRadius, float retryDelayAfterFail, float idleTimeToPatrol, float arrivalThreshold)
        {
            _character = character;
            _patrolRadius = patrolRadius;
            _retryDelayAfterFail = retryDelayAfterFail;
            _idleTimeToPatrol = idleTimeToPatrol;
            _arrivalThreshold = arrivalThreshold;

            _navMeshCharacterController = new NavMeshCharacterController(_character);
            _boredomPatrolController = new BoredomPatrolController(
                    _character,
                    _patrolRadius,
                    _arrivalThreshold,
                    _retryDelayAfterFail
                    );

            _navMeshCharacterController.Enable();
            _boredomPatrolController.Enable();

            _lastDestination = _character.Destination;

            _currentController = _navMeshCharacterController;
        }

        protected override void UpdateLogic(float deltatime)
        {
            HandleControllerSwitching(Time.deltaTime);
            _currentController.Update(Time.deltaTime);
        }

        private void HandleControllerSwitching(float deltaTime)
        {
            Vector3 currentDestination = _character.Destination;

            if ((currentDestination - _lastDestination).magnitude > DestinationChangeThreshold)
            {
                _lastDestination = currentDestination;

                if (_currentController.Equals(_boredomPatrolController))
                {
                    _currentController = _navMeshCharacterController;
                }

                _idleTimer = 0;
                return;
            }

            bool reachedDestination = Vector3.Distance(_character.transform.position, currentDestination) <= _arrivalThreshold;

            if (_currentController.Equals(_boredomPatrolController) == false && reachedDestination && _character.IsWalking == false)
            {
                _idleTimer += deltaTime;

                if (_idleTimer >= _idleTimeToPatrol)
                {
                    _currentController = _boredomPatrolController;
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
