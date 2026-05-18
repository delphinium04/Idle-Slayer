using UnityEngine;

public static class SaveManager
{
    private static string _path = "";

    public static void Save(SaveData data)
    {
        if (data == null) return;
        string json = JsonUtility.ToJson(data, true);

        SetPath();
        System.IO.File.WriteAllText(_path, json);

        Debug.Log($"Saved: {json}");
    }

    public static bool TryLoad(out SaveData saveData)
    {
        saveData = null;
        SetPath();
        if (!System.IO.File.Exists(_path)) return false;

        var json = System.IO.File.ReadAllText(_path);
        saveData = JsonUtility.FromJson<SaveData>(json);

        Debug.Log($"Loaded: {json}");
        return true;
    }

    private static void SetPath()
    {
        if (!string.IsNullOrWhiteSpace(_path)) return;

        _path = System.IO.Path.Combine(
            Application.persistentDataPath,
            "Saved", "SaveData.json");
    }
}