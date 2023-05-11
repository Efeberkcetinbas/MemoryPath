using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class PlayerMovement : MonoBehaviour
{
    private Vector3 firstPosition;
    private Vector3 lastPosition;

    private float dragDistance;
    private float randomNumber;


    public PlayerData playerData;
    public GameData gameData;

    [SerializeField] private ParticleSystem moveParticle,jumpParticle;



    private void Start() 
    {
        dragDistance=Screen.height*15/100;
    }

    void Update()
    {
        CheckMove();

    }

    private void CheckMove()
    {

        if(Input.touchCount>0 && playerData.playerCanMove && !gameData.isGameEnd)
        {
            Touch touch=Input.GetTouch(0);
            if(touch.phase==TouchPhase.Began)
            {
                firstPosition=touch.position;
                lastPosition=touch.position;
            }

            else if(touch.phase==TouchPhase.Moved)
            {
                lastPosition=touch.position;
            }

            else if(touch.phase==TouchPhase.Ended)
            {
                lastPosition=touch.position;

                if(Mathf.Abs(lastPosition.x-firstPosition.x)>Mathf.Abs(lastPosition.y-firstPosition.y))
                {
                    RandomMove();
                    EventManager.Broadcast(GameEvent.OnPlayerMove);
                    if(lastPosition.x>firstPosition.x)
                    {
                        RotateYAxis(90);
                        if(randomNumber == 0 ) GoXAxisWithDash(+2);
                        if(randomNumber == 1) JumpXAxis(+2f,-360,0.5f);
                        //GoXAxisWithDash(+2);
                        CheckDirection(false,false,false,true);
                    }
                    else
                    {
                        RotateYAxis(-90);
                        if(randomNumber == 0 ) GoXAxisWithDash(-2);
                        if(randomNumber == 1) JumpXAxis(-2f,360,0.5f);
                        //GoXAxisWithDash(-2);
                        CheckDirection(false,false,true,false);
                    }
                }

                else
                {
                    if(lastPosition.y>firstPosition.y)
                    {
                        RotateYAxis(0);
                        if(randomNumber == 0 ) GoZAxisWithDash(+2);
                        if(randomNumber == 1) JumpZAxis(+2f,360,0.5f);
                        //GoZAxisWithDash(+2);
                        CheckDirection(true,false,false,false);
                    }
                    else
                    {
                        RotateYAxis(180);
                        if(randomNumber == 0 ) GoZAxisWithDash(-2);
                        if(randomNumber == 1) JumpZAxis(-2f,-360,0.5f);
                        //GoZAxisWithDash(-2);
                        CheckDirection(false,true,false,false);
                    }
                }

                playerData.playerCanMove=false;

            }
        }
    }

    
    #region Move
    private void GoXAxisWithDash(float direction)
    {
        var currentPosLeft=transform.position.x;
        transform.DOMoveX(currentPosLeft+direction,0.1f);
        moveParticle.Play();
    }

    private void GoZAxisWithDash(float direction)
    {
        var currentPosUp=transform.position.z;
        transform.DOMoveZ(currentPosUp+direction,0.1f);
        moveParticle.Play();
    }

    

    #endregion

    #region Direction

    private void CheckDirection(bool up,bool down,bool left,bool right)
    {
        playerData.playerUp=up;
        playerData.playerDown=down;
        playerData.playerLeft=left;
        playerData.playerRight=right;
    }

    #endregion
    
    #region Jump
    private void JumpXAxis(float direction,float rot,float duration)
    {
        var currentPos=transform.position;
        //rotasyon istersen acarsin
        jumpParticle.Play();
        transform.DOScale(Vector3.one/1.5f,duration);
        transform.DOJump(new Vector3(currentPos.x+direction,currentPos.y,currentPos.z),1,1,duration).OnComplete(()=>{
            transform.DOScale(Vector3.one,0.25f);
        });
    }

    private void JumpZAxis(float direction,float rot,float duration)
    {
        var currentPos=transform.position;
        jumpParticle.Play();
        transform.DOScale(Vector3.one/1.5f,duration);
        transform.DOJump(new Vector3(currentPos.x,currentPos.y,currentPos.z + direction),1,1,duration).OnComplete(()=>{
            transform.DOScale(Vector3.one,0.25f);
        });
    }
    #endregion
    private void RandomMove()
    {
        randomNumber=Random.Range(0,2);
        Debug.Log(randomNumber);
    }

    #region  Rotate
    private void RotateYAxis(float y)
    {
        transform.DORotate(new Vector3(0,y,0),0.25f);
    }

    #endregion
}
