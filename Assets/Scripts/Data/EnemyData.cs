using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/EnemyData", order = 3)]
public class EnemyData : ScriptableObject
{
    public string Name;
    public int GoldReward;
    public int Health;
    public int Attack;
    public int Defense;
}