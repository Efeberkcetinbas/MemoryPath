using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GroundTrigger : Obstacable
{
    public GroundTrigger()
    {
        canStay=false;
    }
    [SerializeField] private ParticleSystem exitParticle;
    [SerializeField] private MeshRenderer meshRenderer;

    [SerializeField] private bool canEnter;

    public PlayerData playerData;
    internal override void DoAction(PlayerTrigger player)
    {
        if(canEnter)
        {
            Debug.Log("HIT");
            meshRenderer.material.DOFade(1,1f);
            meshRenderer.material.DOColor(Color.green,1);
            EventManager.Broadcast(GameEvent.OnGround);
            EventManager.Broadcast(GameEvent.OnIncreaseScore);
        }
        
        
    }
//Root.DOLocalRotate(new Vector3(0, 360, 0), .2f, RotateMode.FastBeyond360).SetRelative(true).SetEase(Ease.Linear);
    internal override void InteractionExit(PlayerTrigger player)
    {

        exitParticle.Play();
        if(playerData.playerUp) transform.DOLocalRotate(new Vector3(360,0,0),.5f,RotateMode.FastBeyond360).SetRelative(true).SetEase(Ease.Linear);
        if(playerData.playerDown) transform.DOLocalRotate(new Vector3(-360,0,0),.5f,RotateMode.FastBeyond360).SetRelative(true).SetEase(Ease.Linear);
        if(playerData.playerLeft) transform.DOLocalRotate(new Vector3(0,0,360),.5f,RotateMode.FastBeyond360).SetRelative(true).SetEase(Ease.Linear);
        if(playerData.playerRight) transform.DOLocalRotate(new Vector3(0,0,-360),.5f,RotateMode.FastBeyond360).SetRelative(true).SetEase(Ease.Linear);

    }

    //level basinda acilir
}
