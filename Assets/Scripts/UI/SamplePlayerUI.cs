using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SamplePlayerUI : MonoBehaviour
{
    public PlayerCharacter PlayerCharacter;
    public GoldWallet GoldWallet;
    public AttackUpgradeSystem AttackUpgradeSystem;
    public AttackSpeedUpgradeSystem AttackSpeedUpgradeSystem;
    
    [Header("Texts")]
    [SerializeField] private TextMeshProUGUI gold;
    [SerializeField] private TextMeshProUGUI stats;
    [SerializeField] private TextMeshProUGUI attackLog;
    [SerializeField] private TextMeshProUGUI attackUpgradeCost;
    [SerializeField] private TextMeshProUGUI attackSpeedUpgradeCost;

    [Header("Buttons")]
    [SerializeField] private Button upgradeAttack;
    [SerializeField] private Button upgradeAttackSpeed;

    private void Awake()
    {
        if (GoldWallet != null)
        {
            GoldWallet.OnGoldChanged += RefreshGold;
        }

        if (AttackSpeedUpgradeSystem != null)
        {
            AttackSpeedUpgradeSystem.OnLevelChanged += OnUpgradeSystemChanged;
        }

        if (AttackUpgradeSystem != null)
        {
            AttackUpgradeSystem.OnLevelChanged += OnUpgradeSystemChanged;
        }

        if (AttackUpgradeSystem != null)
        {
            upgradeAttack.onClick.AddListener(UpgradeAttack);
        }

        if (AttackSpeedUpgradeSystem != null)
        {
            upgradeAttackSpeed.onClick.AddListener(UpgradeAttackSpeed);
        }
    }

    private void OnDestroy()
    {
        if (GoldWallet != null)
        {
            GoldWallet.OnGoldChanged -= RefreshGold;
        }

        if (AttackSpeedUpgradeSystem != null)
        {
            AttackSpeedUpgradeSystem.OnLevelChanged -= OnUpgradeSystemChanged;
        }

        if (AttackUpgradeSystem != null)
        {
            AttackUpgradeSystem.OnLevelChanged -= OnUpgradeSystemChanged;
        }

        if (AttackUpgradeSystem != null)
        {
            upgradeAttack.onClick.RemoveListener(UpgradeAttack);
        }

        if (AttackSpeedUpgradeSystem != null)
        {
            upgradeAttackSpeed.onClick.RemoveListener(UpgradeAttackSpeed);
        }
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
        if (PlayerCharacter != null)
        {
            stats.text = $"Attack: {PlayerCharacter.CurrentAttack}\nAttackSpeed: {PlayerCharacter.CurrentAttackSpeed}";
        }
    }

    private void RefreshUpgradeSystem()
    {
        attackUpgradeCost.text = $"Upgrade[ATK] {AttackUpgradeSystem.CurrentUpgradeCost}";
        attackSpeedUpgradeCost.text = $"Upgrade[ATKSPEED]: {AttackSpeedUpgradeSystem.CurrentUpgradeCost}";
    }

    private void UpgradeAttack()
    {
        if (AttackUpgradeSystem == null) return;

        if (AttackUpgradeSystem.TryUpgrade())
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
        if (AttackSpeedUpgradeSystem == null) return;
        
        if (AttackSpeedUpgradeSystem.TryUpgrade())
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