using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerCharacter : MonoBehaviour, IDamageable, IAttacker
{
    public CharacterData Data;

    #region IDamageable

    public float CurrentHealth { get; private set; }
    public float MaxHealth { get; private set; }
    public bool IsAlive { get; private set; }

    public event Action<IDamageable> OnDeath;
    public event Action<DamageInfo> OnDamageTaken;

    public void TakeDamage(DamageInfo result)
    {
        CurrentHealth -= result.Damage;
        OnDamageTaken?.Invoke(result);

        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;
            IsAlive = false;
            OnDeath?.Invoke(this);
        }
    }

    #endregion

    #region IAttacker

    public string AttackerName => Data.Name;

    #endregion

    public float CurrentAttack { get; private set; }
    public float CurrentAttackSpeed { get; private set; }
    public float CurrentCriticalChance { get; private set; }
    public float CurrentCriticalDamage { get; private set; }

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
        YieldInstruction attackYield = new WaitForSeconds(1 / CurrentAttackSpeed);
        float previousAttackSpeed = CurrentAttackSpeed;

        while (IsAlive)
        {
            if (!Mathf.Approximately(previousAttackSpeed, CurrentAttackSpeed))
            {
                attackYield = new WaitForSeconds(1 / CurrentAttackSpeed);
                previousAttackSpeed = CurrentAttackSpeed;
            }

            EnemyCharacter target = _enemyTargetFinder.GetTarget();
            if (target == null) break;

            Attack(target);
            yield return attackYield;
        }
    }

    private void Attack(EnemyCharacter enemy)
    {
        float damage = CurrentAttack;
        bool isCritical = CurrentCriticalChance > Random.Range(0, 1.0f);
        if (isCritical)
        {
            damage *= (100 + CurrentCriticalDamage) * 0.01f;
        }

        DamageInfo damageInfo = new DamageInfo(this, enemy, damage, isCritical);
        enemy.TakeDamage(damageInfo);
    }

    public void IncreaseAttack(int amount)
    {
        CurrentAttack += amount;
    }

    public void IncreaseAttackSpeed(float amount)
    {
        CurrentAttackSpeed += amount;
    }
}