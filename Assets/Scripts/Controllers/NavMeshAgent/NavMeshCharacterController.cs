using UnityEditor.Playables;
using UnityEngine;

namespace LernUnityAdventure_m22_23
{
    public class NavMeshCharacterController : Controller
    {
        private const string LayerMaskName = "WalkGround";
        private const int MoveMouseButton = 1;

        private readonly LayerMask _layerMaskGround;
        private readonly Character _character;
        private readonly ScreenUtility _utility;

        public NavMeshCharacterController(Character character)
        {
            _layerMaskGround = LayerMask.GetMask(LayerMaskName);
            _character = character;
            _utility = new ScreenUtility();

            _character.SetDestination(_character.transform.position);
        }

        protected override void UpdateLogic(float deltatime)
        {
            if (Input.GetMouseButtonDown(MoveMouseButton))
            {
                if (_character.CanMove() == false) return;

                Ray ray = _utility.GetRayFromScreen();

                if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _layerMaskGround))
                {
                    _character.SetDestination(hit.point);
                }
            }
        }
    }
}
