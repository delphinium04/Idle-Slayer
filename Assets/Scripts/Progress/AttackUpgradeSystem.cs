using System;
using UnityEngine;

public class AttackUpgradeSystem : MonoBehaviour
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

    private PlayerCharacter _playerCharacter;
    private GoldWallet _goldWallet;

    private int _level;
    private int _baseUpgradeCost;
    private int _costIncreasePerLevel;
    private int _attackPerLevel;

    private void Awake()
    {
        _baseUpgradeCost = 10;
        _attackPerLevel = 5;
        _costIncreasePerLevel = 20;

        CalculateNeededCost();
    }

    public void Initialize(GameContext context)
    {
        _playerCharacter = context.PlayerCharacter;
        _goldWallet = context.GoldWallet;
    }

    public void SetLevel(int level)
    {
        CurrentLevel = level;
        _playerCharacter.SetAdditiveAttack(_attackPerLevel);
    }

    public bool TryUpgrade()
    {
        CalculateNeededCost();
        if (!_goldWallet.CanSpend(CurrentUpgradeCost)) return false;

        _goldWallet.Spend(CurrentUpgradeCost);
        Upgrade();
        return true;
    }

    private void Upgrade()
    {
        CurrentLevel++;
        _playerCharacter.SetAdditiveAttack(CurrentLevel * _attackPerLevel);
    }

    private void CalculateNeededCost()
    {
        CurrentUpgradeCost = _baseUpgradeCost + CurrentLevel * _costIncreasePerLevel;
    }
    
    
#if UNITY_EDITOR
    private void Update()
    {
        if (UnityEngine.InputSystem.Keyboard.current.f2Key.wasPressedThisFrame)
        {
            CurrentLevel = 0;
        }
    }
#endif
}