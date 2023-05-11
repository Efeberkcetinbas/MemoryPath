using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class GroundTrigger : Obstacable
{
    [SerializeField] private GameObject increaseScorePrefab;
    [SerializeField] private Transform pointPos;

    public GameData gameData;
    public GroundData groundData;
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
            playerData.playerCanMove=true;
            meshRenderer.material.DOFade(1,1f);
            meshRenderer.material.DOColor(Color.green,1);
            groundData.tempPathNumber++;
            StartCoinMove();
            EventManager.Broadcast(GameEvent.OnGround);
            EventManager.Broadcast(GameEvent.OnIncreaseScore);
        }
        else
        {
            meshRenderer.material.DOFade(1,1f);
            meshRenderer.material.DOColor(Color.red,1);
            EventManager.Broadcast(GameEvent.OnGameOver);
        }
        
        
    }
//Root.DOLocalRotate(new Vector3(0, 360, 0), .2f, RotateMode.FastBeyond360).SetRelative(true).SetEase(Ease.Linear);
    internal override void InteractionExit(PlayerTrigger player)
    {
        //ruzgar efekti
        exitParticle.Play();
        if(playerData.playerUp) transform.DOLocalRotate(new Vector3(360,0,0),.5f,RotateMode.FastBeyond360).SetRelative(true).SetEase(Ease.Linear);
        if(playerData.playerDown) transform.DOLocalRotate(new Vector3(-360,0,0),.5f,RotateMode.FastBeyond360).SetRelative(true).SetEase(Ease.Linear);
        if(playerData.playerLeft) transform.DOLocalRotate(new Vector3(0,0,360),.5f,RotateMode.FastBeyond360).SetRelative(true).SetEase(Ease.Linear);
        if(playerData.playerRight) transform.DOLocalRotate(new Vector3(0,0,-360),.5f,RotateMode.FastBeyond360).SetRelative(true).SetEase(Ease.Linear);

    }
    private void StartCoinMove()
    {
        GameObject coin=Instantiate(increaseScorePrefab,pointPos.transform.position,increaseScorePrefab.transform.rotation);
        coin.transform.LookAt(Camera.main.transform);
        var pos=coin.transform.localPosition;
        coin.transform.DOLocalJump(new Vector3(pos.x,pos.y+2,pos.z),1,1,1,false);
        //coin.transform.DOScale(Vector3.zero,1.5f);
        coin.transform.GetChild(0).GetComponent<TextMeshPro>().text=" + " + gameData.increaseScore.ToString();
        coin.transform.GetChild(0).GetComponent<TextMeshPro>().DOFade(0,1.5f).OnComplete(()=>coin.transform.GetChild(0).gameObject.SetActive(false));
        Destroy(coin,2);
    }

    //level basinda acilir
}
