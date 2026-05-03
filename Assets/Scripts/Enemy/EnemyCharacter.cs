using System;
using UnityEngine;

public class EnemyCharacter : MonoBehaviour, IDamageable
{
    public EnemyData Data;

    #region IDamageable

    public float CurrentHealth { get; private set; }
    public float MaxHealth { get; private set; }
    public bool IsAlive { get; private set; }

    public event Action OnDeath;
    public event Action<float> OnDamageTaken;

    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
        OnDamageTaken?.Invoke(damage);

        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;
            IsAlive = false;
            OnDeath?.Invoke();
        }
    }

    #endregion

    public int CurrentStrength { get; private set; }
    public int CurrentDefense { get; private set; }

    private void Awake()
    {
        IsAlive = true;
    }

    void Start()
    {
        if (Data != null)
        {
            CurrentHealth = Data.Health;
            CurrentStrength = Data.Strength;
            CurrentDefense = Data.Defense;
        }
    }
}