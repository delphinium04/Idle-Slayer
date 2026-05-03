using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/EnemyData", order = 3)]
public class EnemyData : ScriptableObject
{
    public string Name;
    public int Gold;
    public int Health;
    public int Strength;
    public int Defense;
}