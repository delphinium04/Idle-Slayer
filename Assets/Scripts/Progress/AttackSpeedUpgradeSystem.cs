using System;
using UnityEngine;

public class AttackSpeedUpgradeSystem : MonoBehaviour
{
    [field: SerializeField] public int CurrentUpgradeCost { get; private set; }
    public int CurrentLevel
    {
        get => _level;
        private set
        {
            if (_level == value) return;

            _level = value;
            CalculateNeededCost();
            OnLevelChanged?.Invoke(_level);
        }
    }

    public event Action<int> OnLevelChanged;

    public PlayerCharacter PlayerCharacter;
    public GoldWallet GoldWallet;

    private int _level;
    private int _baseUpgradeCost;
    private int _costIncreasePerLevel;
    private float _amountPerLevel;

    private void Awake()
    {
        _baseUpgradeCost = 10;
        _costIncreasePerLevel = 5;
        _amountPerLevel = 0.1f;

        CalculateNeededCost();
    }

    public void Initialize(int level)
    {
        CurrentLevel = level;
        PlayerCharacter.SetAdditiveAttackSpeed(_amountPerLevel);
    }

    public bool TryUpgrade()
    {
        CalculateNeededCost();
        if (!GoldWallet.CanSpend(CurrentUpgradeCost)) return false;

        GoldWallet.Spend(CurrentUpgradeCost);
        Upgrade();
        return true;
    }

    private void Upgrade()
    {
        CurrentLevel++;
        PlayerCharacter.SetAdditiveAttackSpeed(CurrentLevel * _amountPerLevel);
    }

    private void CalculateNeededCost()
    {
        CurrentUpgradeCost = _baseUpgradeCost + CurrentLevel * _costIncreasePerLevel;
    }

#if UNITY_EDITOR
    private void Update()
    {
        if (UnityEngine.InputSystem.Keyboard.current.f1Key.wasPressedThisFrame)
        {
            CurrentLevel = 0;
        }
    }
#endif
}