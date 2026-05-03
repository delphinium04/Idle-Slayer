using System;
using UnityEngine;

public class EnemyCharacter : MonoBehaviour
{
    public EnemyData Data;

    public bool IsAlive { get; private set; } = true;

    public float CurrentHealth { get; private set; }
    public int CurrentStrength { get; private set; }
    public int CurrentDefense { get; private set; }

    void Start()
    {
        if (Data != null)
        {
            CurrentHealth = Data.Health;
            CurrentStrength = Data.Strength;
            CurrentDefense = Data.Defense;
        }
    }

    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;
            transform.localScale = Vector3.one * 0.5f;
            IsAlive = false;
        }
    }
}