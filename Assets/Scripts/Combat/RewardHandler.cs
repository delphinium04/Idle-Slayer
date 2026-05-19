using UnityEngine;

public class RewardHandler : MonoBehaviour
{
    public SamplePlayerUI SampleUI;

    GoldWallet _goldWallet;

    public void Initialize(GameContext context)
    {
        _goldWallet = context.GoldWallet;
        EnemyDeathSubscribeTest();
    }

    private void EnemyDeathSubscribeTest()
    {
        var enemies = FindObjectsByType<EnemyCharacter>(FindObjectsSortMode.None);
        foreach (var enemy in enemies)
        {
            enemy.OnDeath += EnemyCharacter_OnDeath;
            enemy.OnDamageTaken += EnemyCharacter_OnDamageTaken;
        }
    }

    private void EnemyCharacter_OnDamageTaken(DamageInfo obj)
    {
        SampleUI.PrintAttackLog(obj);
    }

    private void EnemyCharacter_OnDeath(IDamageable damageable)
    {
        var enemy = damageable as EnemyCharacter;
        if (enemy != null)
        {
            _goldWallet.Add(enemy.Data.GoldReward);
        }
    }
}