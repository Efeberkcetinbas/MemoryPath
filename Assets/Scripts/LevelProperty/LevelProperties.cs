using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelProperties : MonoBehaviour
{
    public GameData gameData;
    public GroundData groundData;

    public List<int> numberOfTrueGrounds=new List<int>();
    private void Start() 
    {
        OnNextLevel();
        
    }
    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnNextLevel,OnNextLevel);
        
    }

    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnNextLevel,OnNextLevel);
    }

    //Her levele ozgu olmasini planladiklarimi buradan atayabilirim.
    private void OnNextLevel()
    {
        groundData.PathNumber=numberOfTrueGrounds[gameData.LevelIndex];
        groundData.tempPathNumber=0;
    }
}
