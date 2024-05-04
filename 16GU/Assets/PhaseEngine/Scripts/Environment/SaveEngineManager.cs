using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveEngineManager : MonoBehaviour
{
    public static SaveEngineManager instance;

    private const string PROGRESS_KEY = "Progress";

    [System.Serializable]
    public class GameData
    {
        public List<CharacterData> character;
    }

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        LoadGameData(out CharacterEngineManager.instance.database);
    }

    // Update is called once per frame
    void Update()
    {
        // autosave
    }

    public void SaveGameData(List<CharacterData> data)
    {
        GameData gameData = new GameData(); 
        gameData.character = data;

        string jsonData = JsonUtility.ToJson(gameData);
        PlayerPrefs.SetString(PROGRESS_KEY, jsonData);

        Debug.Log("Data Saved.");
    }

    public void SaveGameData()
    {
        GameData gameData = new GameData();
        gameData.character = CharacterEngineManager.instance.database;

        string jsonData = JsonUtility.ToJson(gameData); 
        PlayerPrefs.SetString(PROGRESS_KEY, jsonData);

        Debug.Log("Data Saved.");
    }

    public void LoadGameData(out List<CharacterData> characterList)
    {
        if(PlayerPrefs.HasKey(PROGRESS_KEY))
        {
            string jsonData = PlayerPrefs.GetString(PROGRESS_KEY);
            GameData gameData = JsonUtility.FromJson<GameData>(jsonData);
            characterList = gameData.character;
        } else
        {
            characterList = new List<CharacterData>();
        }
    }

    public void ClearAllData()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();

        Debug.Log("All game data cleared.");
    }
}
