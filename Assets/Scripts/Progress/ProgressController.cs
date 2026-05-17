using UnityEngine;

public class ProgressController : MonoBehaviour
{
    [SerializeField] private GoldWallet _goldWallet;

    private SaveData _saveData;

    private void Start()
    {
        if (!SaveManager.TryLoad(out _saveData))
        {
            Debug.LogWarning("Failed to find save data, create new one");
            _saveData = new SaveData
            {
                Gold = 100
            };
        }

        if (_goldWallet != null)
        {
            _goldWallet.Initialize(_saveData.Gold);
            _goldWallet.OnGoldChanged += GoldWallet_OnGoldChanged;
        }
    }

    private void GoldWallet_OnGoldChanged(int gold)
    {
        _saveData.Gold = gold;
        SaveManager.Save(_saveData);
    }
}