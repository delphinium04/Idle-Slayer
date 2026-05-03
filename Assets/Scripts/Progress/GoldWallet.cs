using System;
using UnityEngine;

public class GoldWallet : MonoBehaviour
{
    [field:SerializeField] public int CurrentGold { get; private set; }

    private void Awake()
    {
        CurrentGold = 100;
    }

    public bool CanSpend(int cost)
    {
        return CurrentGold >= cost;
    }

    public void Add(int gold)
    {
        CurrentGold += gold;
    }

    public void Spend(int cost)
    {
        CurrentGold -= cost;
    }
}