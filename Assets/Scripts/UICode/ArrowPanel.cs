using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class ArrowPanel : MonoBehaviour
{
    [SerializeField] private List<Transform> arrowPanel=new List<Transform>(4);
    
    public DirectionData directionData;

    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnPlayerRight,OnPlayerRight);
        EventManager.AddHandler(GameEvent.OnPlayerLeft,OnPlayerLeft);
        EventManager.AddHandler(GameEvent.OnPlayerUp,OnPlayerUp);
        EventManager.AddHandler(GameEvent.OnPlayerDown,OnPlayerDown);
    }

    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnPlayerRight,OnPlayerRight);
        EventManager.RemoveHandler(GameEvent.OnPlayerLeft,OnPlayerLeft);
        EventManager.RemoveHandler(GameEvent.OnPlayerUp,OnPlayerUp);
        EventManager.RemoveHandler(GameEvent.OnPlayerDown,OnPlayerDown);
    }


    void OnPlayerLeft()
    {
        arrowPanel[0].DOScale(Vector3.one*1.5f,0.2f).OnComplete(()=>arrowPanel[0].DOScale(Vector3.one,0.2f));
        directionData.leftNumber--;
        EventManager.Broadcast(GameEvent.OnUIDirectionUpdate);
    }

    void OnPlayerRight()
    {
        arrowPanel[1].DOScale(Vector3.one*1.5f,0.2f).OnComplete(()=>arrowPanel[1].DOScale(Vector3.one,0.2f));
        directionData.rightNumber--;
        EventManager.Broadcast(GameEvent.OnUIDirectionUpdate);
    }

    void OnPlayerUp()
    {
        arrowPanel[2].DOScale(Vector3.one*1.5f,0.2f).OnComplete(()=>arrowPanel[2].DOScale(Vector3.one,0.2f));
        directionData.upNumber--;
        EventManager.Broadcast(GameEvent.OnUIDirectionUpdate);
    }

    void OnPlayerDown()
    {
        arrowPanel[3].DOScale(Vector3.one*1.5f,0.2f).OnComplete(()=>arrowPanel[3].DOScale(Vector3.one,0.2f));
        directionData.downNumber--;
        EventManager.Broadcast(GameEvent.OnUIDirectionUpdate);
    }
    
}
