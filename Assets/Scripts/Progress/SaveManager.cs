using UnityEngine;

public static class SaveManager
{
    private static string _path = "";

    public static void Save(SaveData data)
    {
        string json = UnityEngine.JsonUtility.ToJson(data, true);

        _path = System.IO.Path.Combine(
#if UNITY_EDITOR
            UnityEngine.Application.dataPath,
#else
            UnityEngine.Application.persistentDataPath,
#endif

            "Saved", "SaveData.json");
        System.IO.Directory.CreateDirectory(_path);
        System.IO.File.WriteAllText(_path, json);
    }

    public static SaveData Load()
    {
        // string json = "";
        // if (File.Exists(_path))
        // {
        //     json = File.ReadAllText(_path);
        // }
        // return JsonUtility.FromJson<SaveData>(json);

        return null;
    }
}