using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public GameData gameData;
    public PlayerData playerData;
    public GroundData groundData;

    

    //Game Endte Kaybolmasini istedigin game objectler
    [Header("Game End")]
    public GameObject[] gameObjects;
   

    private void Awake() 
    {
        OnClearData();
    }

    

    private void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnIncreaseScore, OnIncreaseScore);
        EventManager.AddHandler(GameEvent.OnNextLevel,OnClearData);
        EventManager.AddHandler(GameEvent.OnUIGameOver,OnUIGameOver);
        EventManager.AddHandler(GameEvent.OnGameOver,OnGameOver);
    }

    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnIncreaseScore, OnIncreaseScore);
        EventManager.RemoveHandler(GameEvent.OnNextLevel,OnClearData);
        EventManager.RemoveHandler(GameEvent.OnUIGameOver,OnUIGameOver);
        EventManager.RemoveHandler(GameEvent.OnGameOver,OnGameOver);
    }
    
    void OnGameOver()
    {
        playerData.playerCanMove=false;
        gameData.isGameEnd=true;

    }
    

    void OnIncreaseScore()
    {
        DOTween.To(GetScore,ChangeScore,gameData.score+gameData.increaseScore,1f).OnUpdate(UpdateUI);
    }

    private int GetScore()
    {
        return gameData.score;
    }

    private void ChangeScore(int value)
    {
        gameData.score=value;
    }

    private void UpdateUI()
    {
        EventManager.Broadcast(GameEvent.OnUIUpdate);
    }

    private void OnUIGameOver()
    {
        OpenClose(false);
    }
    

    
    void OnClearData()
    {
        gameData.isGameEnd=false;
        OpenClose(true);
    }

    private void OpenClose(bool canOpen)
    {
        for (int i = 0; i < gameObjects.Length; i++)
        {
            if(canOpen)
                gameObjects[i].SetActive(true);
            else
                gameObjects[i].SetActive(false);
        }
    }

    
}
