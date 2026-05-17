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

        void OnUpgradeSystemChanged(int n)
        {
            RefreshUpgradeSystem();
            RefreshStat();
        }
    }

    private void Start()
    {
        if (PlayerCharacter != null)
        {
            RefreshStat();
        }

        if (GoldWallet != null)
        {
            RefreshGold(GoldWallet.CurrentGold);
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
        attackUpgradeCost.text = $"Atk next cost: {AttackUpgradeSystem.CurrentUpgradeCost}";
        attackSpeedUpgradeCost.text = $"AtkSpeed next cost: {AttackSpeedUpgradeSystem.CurrentUpgradeCost}";
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
        ;
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