using System;
using TMPro;
using UnityEngine;

public class SamplePlayerUI : MonoBehaviour
{
    public PlayerCharacter PlayerCharacter;
    public TextMeshProUGUI StrengthUI;
    public TextMeshProUGUI AttackLogUI;

    private void Start()
    {
        if (PlayerCharacter != null)
        {
            StrengthUI.text = $"Strength {PlayerCharacter.CurrentStrength}";
            PlayerCharacter.OnAttackPerformed += PrintAttackLog;
        }
    }

    private void PrintAttackLog(string log)
    {
        AttackLogUI.text = log;
    }
}