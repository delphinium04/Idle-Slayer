using UnityEngine;

public class AttackSpeedUpgradeSystem : MonoBehaviour
{
    [field: SerializeField] public int CurrentUpgradeCost { get; private set; }
    public PlayerCharacter PlayerCharacter;
    public GoldWallet GoldWallet;

    private int _level;
    private int _baseUpgradeCost;
    private int _costIncreasePerLevel;
    private float _amountPerLevel;

    private void Awake()
    {
        _level = 0;
        _baseUpgradeCost = 10;
        _costIncreasePerLevel = 5;
        _amountPerLevel = 0.1f;

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
        _level++;
        PlayerCharacter.IncreaseAttackSpeed(_amountPerLevel);
        CalculateNeededCost();
    }

    private void CalculateNeededCost()
    {
        CurrentUpgradeCost = _baseUpgradeCost + _level * _costIncreasePerLevel;
    }
}