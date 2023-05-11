using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    [Header("Indexes")]
    
    
    public List<GameObject> levels;

    public GameData gameData;

    private void Start()
    {
        LoadLevel();
    }
    private void LoadLevel()
    {


        gameData.LevelIndex = PlayerPrefs.GetInt("LevelNumber");
        if (gameData.LevelIndex == levels.Count) gameData.LevelIndex = 0;
        PlayerPrefs.SetInt("LevelNumber", gameData.LevelIndex);
       

        for (int i = 0; i < levels.Count; i++)
        {
            levels[i].SetActive(false);
        }
        levels[gameData.LevelIndex].SetActive(true);
        
    }

    public void LoadNextLevel()
    {
        PlayerPrefs.SetInt("LevelNumber", gameData.LevelIndex + 1);
        PlayerPrefs.SetInt("RealLevel", PlayerPrefs.GetInt("RealLevel", 0) + 1);
        EventManager.Broadcast(GameEvent.OnNextLevel);
        LoadLevel();
    }

    public void RestartLevel()
    {
        LoadLevel();
    }

    
    
}
