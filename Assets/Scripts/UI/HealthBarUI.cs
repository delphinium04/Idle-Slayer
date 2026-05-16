using UnityEngine;

public class HealthBarUI : MonoBehaviour
{
    private Health _health;
    private UnityEngine.UI.Slider _slider;

    public void Initialize(Health health)
    {
        if (health == null) return;

        _health = health;
        _slider = GetComponentInChildren<UnityEngine.UI.Slider>();
        _health.OnMaxHealthChanged += Health_OnMaxHealthChanged;
        _health.OnHealthChanged += Health_OnHealthChanged;
    }

    private void OnDestroy()
    {
        if (_health == null) return;

        _health.OnMaxHealthChanged -= Health_OnMaxHealthChanged;
        _health.OnHealthChanged -= Health_OnHealthChanged;
    }

    private void Health_OnMaxHealthChanged(float maxHealth)
    {
        _slider.maxValue = maxHealth;
    }

    private void Health_OnHealthChanged(float health)
    {
        _slider.value = health;

        if (!_health.IsAlive)
        {
            _slider.gameObject.SetActive(false);
        }
    }
}