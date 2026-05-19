using System;
using UnityEngine;

public class GoldWallet : MonoBehaviour
{
    public int CurrentGold
    {
        get => _currentGold;
        private set
        {
            if (_currentGold == value) return;

            _currentGold = value;
            OnGoldChanged?.Invoke(CurrentGold);
        }
    }
    private int _currentGold;
    public event Action<int> OnGoldChanged;

    public void Initialize(int gold)
    {
        CurrentGold = gold;
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

    #if UNITY_EDITOR
    private void Update()
    {
        if(UnityEngine.InputSystem.Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Add(150);
        }
    }
    #endif
}