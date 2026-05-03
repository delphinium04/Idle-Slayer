using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SamplePlayerUI : MonoBehaviour
{
    public PlayerCharacter PlayerCharacter;
    public GoldWallet GoldWallet;
    public AttackUpgradeSystem AttackUpgradeSystem;

    [Header("Texts")]
    [SerializeField] private TextMeshProUGUI gold;
    [SerializeField] private TextMeshProUGUI attack;
    [SerializeField] private TextMeshProUGUI attackLog;
    [SerializeField] private TextMeshProUGUI attackUpgradeCost;

    [Header("Buttons")]
    [SerializeField] private Button upgradeStrength;

    private void Start()
    {
        if (PlayerCharacter != null)
        {
            RefreshStat();
            PlayerCharacter.OnAttackPerformed += PrintAttackLog;
        }

        if (GoldWallet != null)
        {
            GoldWallet.OnGoldChanged += RefreshGold;
            RefreshGold(GoldWallet.CurrentGold);
        }

        if (AttackUpgradeSystem != null)
        {
            RefreshUpgradeSystem();
            upgradeStrength.onClick.AddListener(UpgradeStrength);
        }
    }

    private void RefreshGold(int amount)
    {
        gold.text = $"{amount} Gold";
    }

    private void RefreshStat()
    {
        if (PlayerCharacter != null)
        {
            attack.text = $"Attack: {PlayerCharacter.CurrentAttack}";
        }
    }

    private void PrintAttackLog(string log)
    {
        attackLog.text = log;
    }

    private void RefreshUpgradeSystem()
    {
        attackUpgradeCost.text = $"Upgrade cost: {AttackUpgradeSystem.CurrentUpgradeCost}";
    }

    private void UpgradeStrength()
    {
        if (AttackUpgradeSystem != null)
        {
            bool succeed = AttackUpgradeSystem.TryUpgrade();
            if (succeed)
            {
                RefreshStat();
                RefreshUpgradeSystem();
            }
            else
            {
                Debug.Log($"Failed to upgrade");
            }
        }
    }
}