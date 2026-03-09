using UnityEngine;

namespace LernUnityAdventure_m22_23
{
    public class SwitchController : Controller
    {
        private Controller _currentController;
        private Controller _primary;
        private Controller _secondary;

        public SwitchController(Controller primary, Controller secondary)
        {
            _primary = primary;
            _secondary = secondary;

            primary.Enable();
            _currentController = primary;
        }

        public Controller CurrentController => _currentController;

        public void Switch()
        {
            if (_currentController.Equals(_primary)) {
                SecondaryActivation();
            }
            else
            {
                PrimaryActivation();
            }
        }

        protected override void UpdateLogic(float deltatime)
        {
            _currentController.Update(Time.deltaTime);
        }

        private void PrimaryActivation()
        {
            _currentController = _primary;
            _primary.Enable();
            _secondary.Disable();
        }

        private void SecondaryActivation()
        {
            _currentController = _secondary;
            _secondary.Enable();
            _primary.Disable();
        }


    }
}
