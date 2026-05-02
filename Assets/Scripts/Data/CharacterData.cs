using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "ScriptableObjects/CharacterData", order = 1)]
public class CharacterData : ScriptableObject
{
    public string Name;
    public string Description;
    public Sprite Icon;

    public CharacterStats DefaultStats;
    public SkillData Skill;
}