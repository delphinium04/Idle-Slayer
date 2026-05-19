using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SamplePlayerUI : MonoBehaviour
{
    [Header("Texts")]
    [SerializeField] private TextMeshProUGUI gold;
    [SerializeField] private TextMeshProUGUI stats;
    [SerializeField] private TextMeshProUGUI dps;
    [SerializeField] private TextMeshProUGUI attackLog;
    [SerializeField] private TextMeshProUGUI attackUpgradeCost;
    [SerializeField] private TextMeshProUGUI attackSpeedUpgradeCost;

    [Header("Buttons")]
    [SerializeField] private Button upgradeAttack;
    [SerializeField] private Button upgradeAttackSpeed;

    private PlayerCharacter _playerCharacter;
    private GoldWallet _goldWallet;
    private AttackUpgradeSystem _attackUpgradeSystem;
    private AttackSpeedUpgradeSystem _attackSpeedUpgradeSystem;

    public void Initialize(GameContext context)
    {
        _goldWallet = context.GoldWallet;
        _playerCharacter = context.PlayerCharacter;
        _attackSpeedUpgradeSystem = context.AttackSpeedUpgradeSystem;
        _attackUpgradeSystem = context.AttackUpgradeSystem;

        _goldWallet.OnGoldChanged += RefreshGold;
        _attackSpeedUpgradeSystem.OnLevelChanged += OnUpgradeSystemChanged;
        _attackUpgradeSystem.OnLevelChanged += OnUpgradeSystemChanged;

        upgradeAttack.onClick.AddListener(UpgradeAttack);
        upgradeAttackSpeed.onClick.AddListener(UpgradeAttackSpeed);
    }

    private void OnDestroy()
    {
        _goldWallet.OnGoldChanged -= RefreshGold;
        _attackSpeedUpgradeSystem.OnLevelChanged -= OnUpgradeSystemChanged;
        _attackUpgradeSystem.OnLevelChanged -= OnUpgradeSystemChanged;

        upgradeAttack.onClick.RemoveListener(UpgradeAttack);
        upgradeAttackSpeed.onClick.RemoveListener(UpgradeAttackSpeed);
    }

    void OnUpgradeSystemChanged(int n)
    {
        RefreshUpgradeSystem();
        RefreshStat();
    }

    public void PrintAttackLog(DamageInfo log)
    {
        attackLog.text =
            $"{log.Attacker.AttackerName}->{log.Target}: {log.Damage} {(log.IsCritical ? "critical" : "normal")}";
    }

    private void RefreshGold(int amount)
    {
        gold.text = $"{amount} Gold";
    }

    private void RefreshStat()
    {
        if (_playerCharacter != null)
        {
            var atk = _playerCharacter.CurrentAttack;
            var atkSpeed = _playerCharacter.CurrentAttackSpeed;
            var critRate = _playerCharacter.CurrentCriticalChance;
            var critDamage = _playerCharacter.CurrentCriticalDamage;

            var dpsValue = (1 - critRate) * atk * atkSpeed
                           + critRate * atk * atkSpeed * (100 + critDamage) / 100f;

            stats.text =
                $"[Atk] {atk} | [AtkSpeed] {atkSpeed}\n[CritRate] {critRate} | [CritDmg] {critDamage}\nHealth: {_playerCharacter.Health}";
            dps.text = $"DPS: {dpsValue:F2}";
        }
    }

    private void RefreshUpgradeSystem()
    {
        attackUpgradeCost.text = $"Upgrade[ATK] {_attackUpgradeSystem.CurrentUpgradeCost}";
        attackSpeedUpgradeCost.text = $"Upgrade[ATKSPEED]: {_attackSpeedUpgradeSystem.CurrentUpgradeCost}";
    }

    private void UpgradeAttack()
    {
        if (_attackUpgradeSystem == null) return;

        if (_attackUpgradeSystem.TryUpgrade())
        {
            RefreshStat();
            RefreshUpgradeSystem();
        }
        else
        {
            Debug.Log($"Attack upgrading failed: Gold");
        }
    }

    private void UpgradeAttackSpeed()
    {
        if (_attackSpeedUpgradeSystem == null) return;

        if (_attackSpeedUpgradeSystem.TryUpgrade())
        {
            RefreshStat();
            RefreshUpgradeSystem();
        }
        else
        {
            Debug.Log($"Attack speed upgrading failed: Gold");
        }
    }
}