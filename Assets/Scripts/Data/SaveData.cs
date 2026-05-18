using System;
using System.Globalization;

[Serializable]
public class SaveData
{
    public int Gold;
    public int AtkLevel;
    public int AtkSpeedLevel;
    public string LastLoginTimeString;

    public DateTime LastLoginTime
    {
        get => DateTime.TryParse(LastLoginTimeString, out var result) ? result : DateTime.UtcNow;
        set => LastLoginTimeString = value.ToString(CultureInfo.InvariantCulture);
    }

    public static SaveData GetDefault()
    {
        var data = new SaveData
        {
            Gold = 100,
            AtkLevel = 1,
            AtkSpeedLevel = 1,
            LastLoginTime = DateTime.UtcNow
        };
        return data;
    }
}