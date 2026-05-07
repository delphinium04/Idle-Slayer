using UnityEngine;

public class RewardHandler : MonoBehaviour
{
    public GoldWallet GoldWallet;

    private void Start()
    {
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

    public SamplePlayerUI SampleUI;
    private void EnemyCharacter_OnDamageTaken(DamageInfo obj)
    {
        SampleUI.PrintAttackLog(obj);
    }


    private void EnemyCharacter_OnDeath(IDamageable damageable)
    {
        var enemy = damageable as EnemyCharacter;
        if (enemy != null)
        {
            GoldWallet.Add(enemy.Data.GoldReward);
        }
    }
    
}