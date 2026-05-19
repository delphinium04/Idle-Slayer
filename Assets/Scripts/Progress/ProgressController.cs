using System;
using TMPro;
using UnityEngine;

public class ProgressController : MonoBehaviour
{
    [SerializeField] private GoldWallet goldWallet;
    [SerializeField] private AttackSpeedUpgradeSystem atkSpeedUpgradeSystem;
    [SerializeField] private AttackUpgradeSystem atkUpgradeSystem;

    [Header("Offline Popup")]
    [SerializeField] private GameObject goldRewardPopup;
    [SerializeField] private TextMeshProUGUI goldDescription;

    private SaveData _saveData;

    private void Start()
    {
        if (!SaveManager.TryLoad(out _saveData))
        {
            Debug.LogWarning("Failed to find save data, create new one");
            ResetData();
        }

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
        goldWallet.Add(offlineGold);
        goldDescription.text = $"{offlineGold} gold reward";
        goldRewardPopup.SetActive(true);
        Debug.Log($"보상 지급: {offlineGold}");
    }

    private void SubscribeEvents()
    {
        if (goldWallet != null)
        {
            goldWallet.OnGoldChanged += GoldWallet_OnGoldChanged;
        }

        if (atkSpeedUpgradeSystem != null)
        {
            atkSpeedUpgradeSystem.OnLevelChanged += AtkSpeedUpgradeSystem_OnLevelChanged;
        }

        if (atkUpgradeSystem != null)
        {
            atkUpgradeSystem.OnLevelChanged += AtkUpgradeSystem_OnLevelChanged;
        }
    }

    private void OnDestroy()
    {
        if (goldWallet != null)
        {
            goldWallet.OnGoldChanged -= GoldWallet_OnGoldChanged;
        }

        if (atkSpeedUpgradeSystem != null)
        {
            atkSpeedUpgradeSystem.OnLevelChanged -= AtkSpeedUpgradeSystem_OnLevelChanged;
        }

        if (atkUpgradeSystem != null)
        {
            atkUpgradeSystem.OnLevelChanged -= AtkUpgradeSystem_OnLevelChanged;
        }
    }

    private void InitializeData()
    {
        goldWallet?.Initialize(_saveData.Gold);
        atkUpgradeSystem?.Initialize(_saveData.AtkLevel);
        atkSpeedUpgradeSystem?.Initialize(_saveData.AtkSpeedLevel);
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
        SaveManager.Save(SaveData.GetDefault());
    }
#endif
}