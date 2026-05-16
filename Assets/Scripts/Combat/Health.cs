using UnityEngine;

public class Health
{
    public float CurrentHealth
    {
        get => _currentHealth;
        set
        {
            if (Mathf.Approximately(_currentHealth, value)) return;

            _currentHealth = value;
            OnHealthChanged?.Invoke(_currentHealth);
        }
    }
    public float MaxHealth
    {
        get => _maxHealth;
        set
        {
            _maxHealth = value;
            OnMaxHealthChanged?.Invoke(_maxHealth);
        }
    }

    public bool IsAlive => CurrentHealth > 0;

    public event System.Action<float> OnHealthChanged;
    public event System.Action<float> OnMaxHealthChanged;

    private float _currentHealth;
    private float _maxHealth;
}