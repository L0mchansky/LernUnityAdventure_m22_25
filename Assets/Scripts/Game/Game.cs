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

        private void Awake()
        {
            _switchController =
                new SwitchController(
                    _character,
                    _patrolRadius,
                    _retryDelayAfterFail,
                    _idleTimeToPatrol,
                    _arrivalThreshold
                    );

            _switchController.Enable();

        }

        public void Update()
        {
            _switchController.Update(Time.deltaTime);
        }
    }
}
