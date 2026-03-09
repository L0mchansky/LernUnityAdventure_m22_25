namespace LernUnityAdventure_m22_23
{
    public abstract class Controller
    {
        private bool _isEnable;

        public virtual void Enable() => _isEnable = true;

        public virtual void Disable() => _isEnable = false;

        public bool IsEnable => _isEnable;

        public void Update(float deltatime)
        {
            if (_isEnable == false)
                return;

            UpdateLogic(deltatime);
        }

        protected abstract void UpdateLogic(float deltatime);
    }
}
