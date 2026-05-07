using UnityEngine;

public class AttackUpgradeSystem : MonoBehaviour
{
    [field: SerializeField] public int CurrentUpgradeCost { get; private set; }
    public PlayerCharacter PlayerCharacter;
    public GoldWallet GoldWallet;

    private int _level;
    private int _baseUpgradeCost;
    private int _costIncreasePerLevel;
    private int _attackPerLevel;

    private void Awake()
    {
        _level = 0;
        _baseUpgradeCost = 10;
        _attackPerLevel = 5;
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
        _level++;
        PlayerCharacter.IncreaseAttack(_attackPerLevel);
        CalculateNeededCost();
    }

    private void CalculateNeededCost()
    {
        CurrentUpgradeCost = _baseUpgradeCost + _level * _costIncreasePerLevel;
    }
}