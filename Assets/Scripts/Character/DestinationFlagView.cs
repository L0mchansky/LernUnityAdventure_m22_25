using UnityEngine;

namespace LernUnityAdventure_m22_23
{
    public class DestinationFlagView: MonoBehaviour
    {
        [SerializeField] Character _character;
        [SerializeField] GameObject _flagView;

        private const float RangeToDeactivateFlag = 0.5f;

        public void Update()
        {
            if (_character == null) return;
            if (_character.IsLife == false) return;

            ObserveToCharacter();
        }

        private void ObserveToCharacter()
        {
            Vector3 destination = _character.Destination;

            float distance = Vector3.Distance(_character.transform.position, destination);

            if (distance >= RangeToDeactivateFlag)
            {
                SetDestinationFlag(destination);
            }
            else
            {
                _flagView.SetActive(false);
            }
        }

        private void SetDestinationFlag(Vector3 destination)
        {
            //Vector3 flagPosition = new Vector3(destination.x, transform.position.y, destination.z);
            transform.position = destination;
            _flagView.SetActive(true);
        }
    }
}
