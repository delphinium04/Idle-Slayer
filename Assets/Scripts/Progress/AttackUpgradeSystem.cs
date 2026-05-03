using UnityEngine;

public class StrengthUpgradeSystem : MonoBehaviour
{
    [field:SerializeField] public int CurrentUpgradeCost { get; private set; }

    public GoldWallet GoldWallet;
    
    private int _level;
    private int _baseUpgradeCost;
    private int _costIncreasePerLevel;
    private int _strengthIncreasePerLevel;

    private void Awake()
    {
        _level = 0;
        _baseUpgradeCost = 10;
        _strengthIncreasePerLevel = 5;
        _costIncreasePerLevel = 20;
        
        CalculateNeededCost();
    }

    public bool TryUpgrade()
    {
        CalculateNeededCost();

        if (GoldWallet.CanSpend(CurrentUpgradeCost))
        {
            GoldWallet.Spend(CurrentUpgradeCost);
            Upgrade();
            return true;
        }

        return false;
    }

    private void Upgrade()
    {
        // Strength Plus
    }

    private void CalculateNeededCost()
    {
        CurrentUpgradeCost = _baseUpgradeCost + _level * _costIncreasePerLevel;
    }
}