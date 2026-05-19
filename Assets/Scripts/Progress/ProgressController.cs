using System;
using TMPro;
using UnityEngine;

public class ProgressController : MonoBehaviour
{
    [Header("Offline Popup")]
    [SerializeField] private GameObject goldRewardPopup;
    [SerializeField] private TextMeshProUGUI goldDescription;

    private SaveData _saveData;
    private GoldWallet _goldWallet;
    private AttackSpeedUpgradeSystem _asus;
    private AttackUpgradeSystem _aus;

    public void Initialize(GameContext context)
    {
        _saveData = context.SaveData;
        _goldWallet = context.GoldWallet;
        _asus = context.AttackSpeedUpgradeSystem;
        _aus = context.AttackUpgradeSystem;

        InitializeData();
        SubscribeEvents();
        RewardOfflineTime();
    }

    private void RewardOfflineTime()
    {
        var currentTime = DateTime.UtcNow;
        TimeSpan offlineTime = currentTime - _saveData.LastLoginTime;
        Debug.Log($"미접속 시간: {offlineTime.Hours}:{offlineTime.Minutes}:{offlineTime.Seconds}");

        if (offlineTime.TotalSeconds < 10)
        {
            goldRewardPopup.SetActive(false);
            return;
        }

        var offlineGold = (int)offlineTime.TotalMinutes;
        _goldWallet.Add(offlineGold);
        goldDescription.text = $"{offlineGold} gold reward";
        goldRewardPopup.SetActive(true);
        Debug.Log($"보상 지급: {offlineGold}");
    }

    private void SubscribeEvents()
    {
        _goldWallet.OnGoldChanged += GoldWallet_OnGoldChanged;
        _asus.OnLevelChanged += AtkSpeedUpgradeSystem_OnLevelChanged;
        _aus.OnLevelChanged += AtkUpgradeSystem_OnLevelChanged;
    }

    private void OnDestroy()
    {
        _goldWallet.OnGoldChanged -= GoldWallet_OnGoldChanged;
        _asus.OnLevelChanged -= AtkSpeedUpgradeSystem_OnLevelChanged;
        _aus.OnLevelChanged -= AtkUpgradeSystem_OnLevelChanged;
    }

    private void InitializeData()
    {
        _goldWallet.Initialize(_saveData.Gold);
        _aus.SetLevel(_saveData.AtkLevel);
        _asus.SetLevel(_saveData.AtkSpeedLevel);
    }

    private void AtkSpeedUpgradeSystem_OnLevelChanged(int level)
    {
        _saveData.AtkSpeedLevel = level;
        SaveManager.Save(_saveData);
    }

    private void AtkUpgradeSystem_OnLevelChanged(int level)
    {
        _saveData.AtkLevel = level;
        SaveManager.Save(_saveData);
    }

    private void GoldWallet_OnGoldChanged(int gold)
    {
        _saveData.Gold = gold;
        SaveManager.Save(_saveData);
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (_saveData == null) return;
        _saveData.LastLoginTime = System.DateTime.UtcNow;
        SaveManager.Save(_saveData);
    }

    private void OnApplicationQuit()
    {
        _saveData.LastLoginTime = System.DateTime.UtcNow;
        SaveManager.Save(_saveData);
    }

#if UNITY_EDITOR
    private void Update()
    {
        if (UnityEngine.InputSystem.Keyboard.current.rKey.wasPressedThisFrame)
        {
            ResetData();
            InitializeData();
        }
    }

    [ContextMenu("Reset Data")]
    public void ResetData()
    {
        var saveData = SaveData.GetDefault();
        SaveManager.Save(saveData);
        _saveData = saveData;
    }
#endif
}