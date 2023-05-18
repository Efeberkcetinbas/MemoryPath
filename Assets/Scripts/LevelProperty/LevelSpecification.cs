using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpecification : MonoBehaviour
{
    public DirectionData directionData;

    [SerializeField] private int leftNumber,rightNumber,upNumber,downNumber;
    [SerializeField] private string directionValue;

    private void OnEnable() {
        EventManager.AddHandler(GameEvent.OnNextLevel,OnNextLevel);
    }

    private void OnDisable() {
        EventManager.RemoveHandler(GameEvent.OnNextLevel,OnNextLevel);
    }

    private void Start() {
        OnNextLevel();
    }
    private void OnNextLevel()
    {
        directionData.directionText=directionValue;
        directionData.leftNumber=leftNumber;
        directionData.rightNumber=rightNumber;
        directionData.upNumber=upNumber;
        directionData.downNumber=downNumber;
        EventManager.Broadcast(GameEvent.OnUIDirectionUpdate);
    }
}
