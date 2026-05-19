using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DamageFloatingHandler : MonoBehaviour
{
    [SerializeField] private DamageFloatingPopup popupPrefab;

    EnemyCharacter[] _enemies;

    private void Start()
    {
        _enemies = FindObjectsByType<EnemyCharacter>(FindObjectsSortMode.None);
        foreach (var enemyCharacter in _enemies)
        {
            enemyCharacter.OnDamageTaken += EnemyCharacter_OnOnDamageTaken;
        }
    }

    private void EnemyCharacter_OnOnDamageTaken(DamageInfo damageInfo)
    {
        var popup = Instantiate(popupPrefab);
        var position = (damageInfo.Target as MonoBehaviour)?.transform.position ?? Vector3.zero;
        position += new Vector3(Random.Range(-.5f, .5f), 1.5f, 0);
        popup.transform.position = position;
        
        popup.Initialize(damageInfo.Damage, damageInfo.IsCritical);
    }
}