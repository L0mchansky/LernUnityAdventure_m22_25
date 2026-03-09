using UnityEngine;

namespace LernUnityAdventure_m22_23
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private Character _character;

        [SerializeField] private float _patrolRadius;
        [SerializeField] private float _retryDelayAfterFail;
        [SerializeField] private float _idleTimeToPatrol;
        [SerializeField] private float _arrivalThreshold;

        private SwitchController _switchController;

        private float _idleTimer;
        private Vector3 _lastDestination;
        private const float DestinationChangeThreshold = 0.3f;
        private Controller _navMeshCharacterController;
        private Controller _boredomPatrolController;

        private void Awake()
        {
            NavMeshCharacterController navMeshCharacterController = new NavMeshCharacterController(_character);
            BoredomPatrolController boredomPatrolController = new BoredomPatrolController(
                    _character,
                    _patrolRadius,
                    _arrivalThreshold,
                    _retryDelayAfterFail
                    );

            _navMeshCharacterController = navMeshCharacterController;
            _boredomPatrolController = boredomPatrolController;

            _switchController = new SwitchController(navMeshCharacterController, boredomPatrolController);
            _switchController.Enable();
        }

        public void Update()
        {
            if (_character == null) return;
            if (_character.IsLife == false) return;

            HandleControllerSwitching();
            _switchController.Update(Time.deltaTime);
        }

        private void HandleControllerSwitching()
        {
            Vector3 currentDestination = _character.Destination;

            if ((currentDestination - _lastDestination).magnitude > DestinationChangeThreshold)
            {
                _lastDestination = currentDestination;

                if (_boredomPatrolController.IsEnable)
                {
                    _switchController.Switch();
                }

                _idleTimer = 0;
                return;
            }

            if (_navMeshCharacterController.IsEnable && ReachedDestination(currentDestination) && _character.IsWalking == false)
            {
                _idleTimer += Time.deltaTime;

                if (_idleTimer >= _idleTimeToPatrol)
                {
                    _switchController.Switch();
                    _idleTimer = 0;
                }
            }
            else
            {
                _idleTimer = 0;
            }
        }

        private bool ReachedDestination(Vector3 currentDestination) 
            => Vector3.Distance(_character.transform.position, currentDestination) <= _arrivalThreshold;
    }
}
