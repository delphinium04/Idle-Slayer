using UnityEngine;

public class Bootstrapper : MonoBehaviour
{
    [Header("Player Data")]
    public PlayerCharacter PlayerPrefab;
    public CharacterData CharacterData;

    [Header("Components")]
    public ProgressController ProgressController;
    public GoldWallet GoldWallet;
    public RewardHandler RewardHandler;
    public AttackSpeedUpgradeSystem Asus;
    public AttackUpgradeSystem Aus;

    [Header("UI")]
    public SamplePlayerUI PlayerUI;

    private void Start()
    {
        GameContext gameContext = new();

        if (!SaveManager.TryLoad(out gameContext.SaveData))
        {
            Debug.LogWarning("Failed to find save data, create new one");
            gameContext.SaveData = SaveData.GetDefault();
            SaveManager.Save(gameContext.SaveData);
        }

        var character = Instantiate(PlayerPrefab, Vector3.zero, Quaternion.identity);
        var model = Instantiate(CharacterData.Model, character.transform);
        model.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);

        gameContext.CharacterData = CharacterData;
        gameContext.PlayerCharacter = character;
        gameContext.GoldWallet = GoldWallet;
        gameContext.RewardHandler = RewardHandler;
        gameContext.AttackSpeedUpgradeSystem = Asus;
        gameContext.AttackUpgradeSystem = Aus;

        Asus.Initialize(gameContext);
        Aus.Initialize(gameContext);
        RewardHandler.Initialize(gameContext);
        ProgressController.Initialize(gameContext);

        PlayerUI.Initialize(gameContext);
    }
}