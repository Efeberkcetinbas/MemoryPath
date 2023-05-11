using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    public Camera mainCamera;

    public CinemachineVirtualCamera cm;

    Vector3 cameraInitialPosition;

    [Header("Shake Control")]
    public float shakeMagnitude = 0.05f;
    public float shakeTime = 0.5f;

    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnGameOver,GameOver);
        EventManager.AddHandler(GameEvent.OnGround,OnGround);
        EventManager.AddHandler(GameEvent.OnPlayerMove,OnPlayerMove);
    }

    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnGameOver,GameOver);
        EventManager.RemoveHandler(GameEvent.OnGround,OnGround);
        EventManager.RemoveHandler(GameEvent.OnPlayerMove,OnPlayerMove);
    }

    

    private void OnGround()
    {
        ChangeFieldOfView(55,0.2f);
    }

    private void OnPlayerMove()
    {
        ShakeIt();
    }

    

    
    public void ChangeFieldOfView(float fieldOfView, float duration = 1)
    {
        DOTween.To(() => cm.m_Lens.FieldOfView, x => cm.m_Lens.FieldOfView = x, fieldOfView, duration).OnComplete(()=>{
            ResetFieldOfView(60,0.2f);
        });
    }

    private void ResetFieldOfView(float fieldOfView, float duration = 1)
    {
        DOTween.To(() => cm.m_Lens.FieldOfView, x => cm.m_Lens.FieldOfView = x, fieldOfView, duration);
    }
   
    public void ResetCamera()
    {
        cm.m_Priority = 1;
    }

    void GameOver()
    {
        DOTween.To(() => cm.m_Lens.FieldOfView, x => cm.m_Lens.FieldOfView = x, 80, 0.5f).OnComplete(()=>
        {
            EventManager.Broadcast(GameEvent.OnUIGameOver);
        });
        
    }


    #region CameraShaker

    private void ShakeIt()
    {
        cameraInitialPosition = mainCamera.transform.position;
        InvokeRepeating("StartCameraShaking", 0f, 0.005f);
        Invoke("StopCameraShaking", shakeTime);

    }

    private void StartCameraShaking()
    {
        float cameraShakingOffsetX = Random.value * shakeMagnitude * 2 - shakeMagnitude;
        float cameraShakingOffsetY = Random.value * shakeMagnitude * 2 - shakeMagnitude;
        Vector3 cameraIntermediatePosition = mainCamera.transform.position;
        cameraIntermediatePosition.x += cameraShakingOffsetX;
        cameraIntermediatePosition.y += cameraShakingOffsetY;
        mainCamera.transform.position = cameraIntermediatePosition;
    }

    private void StopCameraShaking()
    {
        CancelInvoke("StartCameraShaking");
        mainCamera.transform.position = cameraInitialPosition;
    }
    #endregion    
}
