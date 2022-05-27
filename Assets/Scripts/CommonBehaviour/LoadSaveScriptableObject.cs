using System.IO;
using UnityEngine;

public class LoadSaveScriptableObject : ScriptableObject
{
    protected void SaveToFile(string fileName)
    {
        string filePath = Path.Combine(Application.persistentDataPath, fileName);

        File.WriteAllText(filePath, JsonUtility.ToJson(this));
    }

    protected void LoadFromFile(string fileName)
    {
        string filePath = Path.Combine(Application.persistentDataPath, fileName);

        if (!File.Exists(filePath))
        {
            Debug.LogWarning($"File {filePath} not found!", this);
            return;
        }
        
        JsonUtility.FromJsonOverwrite(File.ReadAllText(filePath), this);
    }
}
