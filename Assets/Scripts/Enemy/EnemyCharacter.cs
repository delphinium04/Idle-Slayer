using System;
using UnityEngine;

public class EnemyCharacter : MonoBehaviour, IDamageable, IAttacker
{
    public EnemyData Data;

    #region IDamageable

    public float CurrentHealth { get; private set; }
    public float MaxHealth { get; private set; }
    public bool IsAlive { get; private set; }

    public event Action<IDamageable> OnDeath;
    public event Action<DamageInfo> OnDamageTaken;

    public void TakeDamage(DamageInfo result)
    {
        if (CurrentHealth == 0) return;

        CurrentHealth -= result.Damage;
        OnDamageTaken?.Invoke(result);

        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;
            IsAlive = false;
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
        IsAlive = true;
    }

    void Start()
    {
        if (Data != null)
        {
            CurrentHealth = Data.Health;
            CurrentAttack = Data.Attack;
            CurrentDefense = Data.Defense;
        }
    }
}