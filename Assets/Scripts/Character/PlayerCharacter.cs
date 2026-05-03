using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerCharacter : MonoBehaviour, IDamageable
{
    public CharacterData Data;

    #region IDamageable

    public float CurrentHealth { get; private set; }
    public float MaxHealth { get; private set; }
    public bool IsAlive { get; private set; }

    public event Action<IDamageable> OnDeath;
    public event Action<float> OnDamageTaken;

    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
        OnDamageTaken?.Invoke(damage);

        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;
            IsAlive = false;
            OnDeath?.Invoke(this);
        }
    }

    #endregion

    public float CurrentAttack { get; private set; }
    public float CurrentAttackSpeed { get; private set; }
    public float CurrentCriticalChance { get; private set; }
    public float CurrentCriticalDamage { get; private set; }

    public Action<string> OnAttackPerformed;

    private EnemyTargetFinder _enemyTargetFinder;

    private void Awake()
    {
        _enemyTargetFinder = GetComponent<EnemyTargetFinder>();

        IsAlive = true;
    }

    private void Start()
    {
        if (Data != null)
        {
            InitData();
            StartCoroutine(AttackRoutine());
        }
    }

    private void InitData()
    {
        MaxHealth = Data.DefaultStats.Health;
        CurrentHealth = Data.DefaultStats.Health;
        CurrentAttack = Data.DefaultStats.Attack;
        CurrentAttackSpeed = Data.DefaultStats.AttackSpeed;
        CurrentCriticalChance = Data.DefaultStats.CriticalChance;
        CurrentCriticalDamage = Data.DefaultStats.CriticalDamage;
    }

    private IEnumerator AttackRoutine()
    {
        YieldInstruction wait = new WaitForSeconds(1 / CurrentAttackSpeed);
        for (int i = 0; i < 100; i++)
        {
            EnemyCharacter target = _enemyTargetFinder.GetTarget();
            if (target == null) break;

            Attack(target);
            yield return wait;
        }
        
        OnAttackPerformed?.Invoke("Attack End");
    }

    private void Attack(EnemyCharacter enemy)
    {
        float damage = CurrentAttack;
        bool isCritical = CurrentCriticalChance > Random.Range(0, 1.0f);
        if (isCritical)
        {
            damage *= (100 + CurrentCriticalDamage) * 0.01f;
        }

        string message = $"{(isCritical ? "critical" : "")} attacked {damage} damage to {enemy.name}";
        enemy.TakeDamage(damage);
        OnAttackPerformed?.Invoke(message);
    }
}