using System;
using UnityEngine;

public class ProgressController : MonoBehaviour
{
    [SerializeField] private GoldWallet _goldWallet;

    private SaveData _saveData;

    private void Awake()
    {
        _saveData = new SaveData()
        {
            Gold = 0,
            AtkLevel = 0,
            AtkSpeedLevel = 0,
            LastLoginTime = null
        };

        if (_goldWallet != null)
        {
            _goldWallet.OnGoldChanged += GoldWallet_OnGoldChanged;
        }
        
        Debug.Log(Application.persistentDataPath);
        Debug.Log(Application.dataPath);
    }

    private void GoldWallet_OnGoldChanged(int gold)
    {
        _saveData.Gold = gold;
        SaveManager.Save(_saveData);
    }
}