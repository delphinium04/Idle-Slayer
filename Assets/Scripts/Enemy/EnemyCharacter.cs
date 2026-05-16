using System;
using UnityEngine;

public class EnemyCharacter : MonoBehaviour, IDamageable, IAttacker
{
    [field: SerializeField] public EnemyData Data { get; private set; }
    public Health Health { get; private set; }

    [SerializeField] private HealthBarUI _healthBarUI;

    #region IDamageable

    public event Action<IDamageable> OnDeath;
    public event Action<DamageInfo> OnDamageTaken;

    public void TakeDamage(DamageInfo result)
    {
        if (!Health.IsAlive) return;

        Health.CurrentHealth -= result.Damage;
        OnDamageTaken?.Invoke(result);

        if (!Health.IsAlive)
        {
            OnDeath?.Invoke(this);
            transform.localScale = Vector3.one * 0.5f;
        }
    }

    #endregion

    #region IAttacker

    public string AttackerName => Data.Name;

    #endregion

    public int CurrentAttack { get; private set; }
    public int CurrentDefense { get; private set; }

    private void Awake()
    {
        Health = new Health();

        // Temp health bar
        if (_healthBarUI != null)
        {
            _healthBarUI.Initialize(Health);
        }
    }

    void Start()
    {
        if (Data != null)
        {
            Health.MaxHealth = Data.Health;
            Health.CurrentHealth = Data.Health;
        }
    }
}