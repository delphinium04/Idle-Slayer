using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerCharacter : MonoBehaviour, IDamageable, IAttacker
{
    public CharacterData Data;

    public Health Health { get; private set; }

    public float CurrentAttack { get; private set; }
    public float CurrentAttackSpeed { get; private set; }
    public float CurrentCriticalChance { get; private set; }
    public float CurrentCriticalDamage { get; private set; }

    private EnemyTargetFinder _enemyTargetFinder;

    #region IDamageable

    public event Action<IDamageable> OnDeath;
    public event Action<DamageInfo> OnDamageTaken;

    public void TakeDamage(DamageInfo result)
    {
        Health.CurrentHealth -= result.Damage;
        OnDamageTaken?.Invoke(result);

        if (Health.IsAlive)
        {
            OnDeath?.Invoke(this);
        }
    }

    #endregion

    #region IAttacker

    public string AttackerName => Data.Name;

    #endregion

    private void Awake()
    {
        _enemyTargetFinder = GetComponent<EnemyTargetFinder>();
        Health = new Health();
        if (Data != null)
        {
            InitData();
        }
    }

    private void Start()
    {
        StartCoroutine(AttackRoutine());
    }

    private void InitData()
    {
        Health.MaxHealth = Data.DefaultStats.Health;
        Health.CurrentHealth = Data.DefaultStats.Health;
        CurrentAttack = Data.DefaultStats.Attack;
        CurrentAttackSpeed = Data.DefaultStats.AttackSpeed;
        CurrentCriticalChance = Data.DefaultStats.CriticalChance;
        CurrentCriticalDamage = Data.DefaultStats.CriticalDamage;
    }

    private IEnumerator AttackRoutine()
    {
        YieldInstruction attackYield = new WaitForSeconds(1 / CurrentAttackSpeed);
        float previousAttackSpeed = CurrentAttackSpeed;

        while (Health.IsAlive)
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
        CurrentAttack = Data.DefaultStats.Attack + amount;
    }

    public void IncreaseAttackSpeed(float amount)
    {
        CurrentAttackSpeed = Data.DefaultStats.AttackSpeed + amount;
    }
}