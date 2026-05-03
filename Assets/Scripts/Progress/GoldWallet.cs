using System;
using UnityEngine;

public class GoldWallet : MonoBehaviour
{
    public int CurrentGold { get; private set; }
    public event Action<int> OnGoldChanged;

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
        OnGoldChanged?.Invoke(CurrentGold);
    }

    public void Spend(int cost)
    {
        CurrentGold -= cost;
        OnGoldChanged?.Invoke(CurrentGold);
    }
}