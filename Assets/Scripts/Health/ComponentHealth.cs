namespace LernUnityAdventure_m22_23
{
    public class ComponentHealth
    {
        private const float MaxPercentage = 100f;

        private float _maxHealth;
        private float _currentHealth;
        private float _percentageHealth;
        private bool _isLife = true;

        public ComponentHealth(float maxHealth)
        {
            _maxHealth = maxHealth;
            _currentHealth = maxHealth;
            _percentageHealth = MaxPercentage;
        }

        public float MaxHealth => _maxHealth;
        public float CurrentHealth => _currentHealth;
        public bool IsLife => _isLife;
        public float PercentageHealth => _percentageHealth;

        public void SetHealth(float newHealth)
        {
            if (newHealth <= 0)
            {
                _currentHealth = 0;
                _percentageHealth = 0;
                _isLife = false;
            }
            else if (newHealth > _maxHealth)
            {
                _currentHealth = _maxHealth;
                _percentageHealth = MaxPercentage;
            }
            else
            {
                _currentHealth = newHealth;
                _percentageHealth = CalculatePercentage();
            }
        }

        private float CalculatePercentage()
        {
            return MaxPercentage / _maxHealth * _currentHealth;
        }
    }
}
