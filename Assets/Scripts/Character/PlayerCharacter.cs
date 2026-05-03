using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerCharacter : MonoBehaviour
{
    public CharacterData Data;

    public float CurrentHealth { get; private set; }
    public float CurrentStrength { get; private set; }
    public float CurrentAttackSpeed { get; private set; }
    public float CurrentCriticalChance { get; private set; }
    public float CurrentCriticalDamage { get; private set; }

    public Action<string> OnAttackPerformed;

    private EnemyTargetFinder _enemyTargetFinder;

    private void Awake()
    {
        _enemyTargetFinder = GetComponent<EnemyTargetFinder>();
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
        CurrentHealth = Data.DefaultStats.Health;
        CurrentStrength = Data.DefaultStats.Strength;
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
    }

    private void Attack(EnemyCharacter enemy)
    {
        float damage = CurrentStrength;
        bool isCritical = CurrentCriticalChance > Random.Range(0, 1.0f);
        if (isCritical)
        {
            damage *= (100 + CurrentCriticalDamage) * 0.01f;
        }

        string message = $"{(isCritical ? "critical" : "")} attacked (<color=red>{damage}</color>) to {enemy.name}";
        enemy.TakeDamage(damage);
        OnAttackPerformed?.Invoke(message);
    }
}