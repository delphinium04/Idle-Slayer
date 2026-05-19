using UnityEngine;

[System.Serializable]
public class CharacterStats
{
    [Header("체력: int")]
    public int Health;
    
    [Header("공격력: int")]
    public int Attack;
    
    [Header("치명타 확률: float(0~1)")]
    public float CriticalChance;
    
    [Header("치명타 추가 데미지: float(100 + N)%")]
    public float CriticalDamage;

    [Header("공격 속도: float(0~)")]
    public float AttackSpeed;
}