using System.IO;
using UnityEngine;

public static class GameSaver
{
    public static void Save<T>(T data)
    {
        string path = GetPath<T>();
        // гарантия, что папка существует
        Directory.CreateDirectory(Path.GetDirectoryName(path));

        string json = JsonUtility.ToJson(data, true);
        string tmp = path + ".tmp";
        File.WriteAllText(tmp, json);
        if (File.Exists(path)) File.Delete(path);
        File.Move(tmp, path);

        Debug.Log($"[GameSaver] Saved {typeof(T).Name} => {path}");
    }

    public static bool TryLoad<T>(out T data) where T : new()
    {
        string path = GetPath<T>();
        if (File.Exists(path))
        {
            try
            {
                string json = File.ReadAllText(path);
                data = JsonUtility.FromJson<T>(json);
                if (data == null) data = new T();
                Debug.Log($"[GameSaver] Loaded {typeof(T).Name} <= {path}");
                return true;
            }
            catch (System.Exception e)
            {
                Debug.LogWarning($"[GameSaver] Read file error: {e.Message}\nPath: {path}");
            }
        }
        data = new T();
        return false;
    }

    private static string GetPath<T>()
    {
        return Path.Combine(Application.persistentDataPath, typeof(T).Name + ".json");
    }
}
