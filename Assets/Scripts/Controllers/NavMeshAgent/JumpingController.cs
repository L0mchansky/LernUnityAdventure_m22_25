using LernUnityAdventure_m22_23;
using UnityEngine.AI;

namespace LernUnityAdventure_m24_25
{
    public class JumpingController : Controller
    {
        private Character _character;

        public JumpingController(Character character)
        {
            _character = character;
        }
        protected override void UpdateLogic(float deltatime)
        {
            OnJumping();
        }

        private void OnJumping()
        {
            if (InOnNavMeshLink(out OffMeshLinkData offMeshLinkData))
            {
                _character.AgentJumper.Jump(offMeshLinkData);
            }
        }

        private bool InOnNavMeshLink(out OffMeshLinkData offMeshLinkData)
        {
            if (_character.IsOnOffMeshLink)
            {
                offMeshLinkData = _character.CurrentOffMeshLinkData;
                return true;
            }

            offMeshLinkData = default;
            return false;
        }
    }
}