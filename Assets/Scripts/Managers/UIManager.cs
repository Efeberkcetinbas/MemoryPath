using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI score;
    public TextMeshProUGUI fromText;
    public TextMeshProUGUI toText;

    public Image progressBar;

    public GameData gameData;
    public GroundData groundData;

    private void Start() 
    {
        OnUIUpdate();
        OnUIUpdateLevelText();
        
    }
    private void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnUIUpdate, OnUIUpdate);
        EventManager.AddHandler(GameEvent.OnNextLevel,OnUIUpdateLevelText);
        EventManager.AddHandler(GameEvent.OnGround,OnUIUpdateGroundNumber);
    }
    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnUIUpdate, OnUIUpdate);
        EventManager.RemoveHandler(GameEvent.OnNextLevel,OnUIUpdateLevelText);
        EventManager.RemoveHandler(GameEvent.OnGround,OnUIUpdateGroundNumber);
    }

    
    void OnUIUpdate()
    {
        score.SetText(gameData.score.ToString());
        score.transform.DOScale(new Vector3(1.5f,1.5f,1.5f),0.2f).OnComplete(()=>score.transform.DOScale(new Vector3(1,1f,1f),0.2f));
    }

    void OnUIUpdateLevelText()
    {
        fromText.SetText((gameData.LevelIndex+1).ToString());
        toText.SetText((gameData.LevelIndex+2).ToString());
    }
    
    void OnUIUpdateGroundNumber()
    {
        float amount=(float)groundData.tempPathNumber/(float)groundData.PathNumber;
        progressBar.DOFillAmount(amount,0.2f);
    }

    
}
