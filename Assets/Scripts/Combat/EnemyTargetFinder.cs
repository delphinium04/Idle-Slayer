using UnityEngine;

public class EnemyTargetFinder : MonoBehaviour
{
    public EnemyCharacter GetTarget()
    {
        var enemies = FindObjectsByType<EnemyCharacter>(FindObjectsSortMode.None);
        if (enemies.Length == 0) return null;

        var returnIndex = -1;
        var minDistance = -1f;
        for (int i = 0; i < enemies.Length; i++)
        {
            if (!enemies[i].IsAlive) continue;

            var dist = (transform.position - enemies[i].transform.position).sqrMagnitude;
            if (dist < minDistance || returnIndex == -1)
            {
                minDistance = dist;
                returnIndex = i;
            }
        }

        return returnIndex == -1 ? null : enemies[returnIndex];
    }
}