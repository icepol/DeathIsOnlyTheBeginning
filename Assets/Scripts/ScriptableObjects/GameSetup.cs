using pixelook;
using UnityEngine;

[CreateAssetMenu(fileName = "GameSetup", menuName = "Assets/Game Setup")]
public class GameSetup : LoadSaveScriptableObject, IResetBeforeBuild
{
    private const string FILENAME = "game_setup.json";
    
    [Header("Skin Settings")]
    public int selectedSkinIndex;
    public bool areUnlockedAll;

    public int SelectedSkinIndex
    {
        get => selectedSkinIndex;
        set
        {
            selectedSkinIndex = value;
            
            SaveToFile(FILENAME);
        }
    }

    public bool AreUnlockedAll
    {
        get => areUnlockedAll;
        set
        {
            areUnlockedAll = value;
            
            SaveToFile(FILENAME);
        }
    }

    [Header("Spawn Settings")]
    public int minSpawnedSquadrons;
    public int minSquadronCountToSpawnEnemies = 5;
    public int minSquadronCountToSpawnCities = 10;

    [Header("Build setup")]
    public bool isProduction;

    public void LoadFromFile()
    {
        LoadFromFile(FILENAME);
    }

    public void ResetBeforeBuild()
    {
        if (!isProduction) return;
        
        SelectedSkinIndex = 0;
        AreUnlockedAll = false;
    }
}
