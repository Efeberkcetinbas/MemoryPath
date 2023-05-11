using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Game Scene UI")]
    public TextMeshProUGUI score;
    public TextMeshProUGUI fromText;
    public TextMeshProUGUI toText;

    public Image progressBar;

    [Header("Data")]
    public GameData gameData;
    public GroundData groundData;

    [Header("Game End UI")] 
    public RectTransform failPanel;
    [SerializeField] private float x,y;

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
        EventManager.AddHandler(GameEvent.OnUIGameOver,OnUIGameOver);
    }
    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnUIUpdate, OnUIUpdate);
        EventManager.RemoveHandler(GameEvent.OnNextLevel,OnUIUpdateLevelText);
        EventManager.RemoveHandler(GameEvent.OnGround,OnUIUpdateGroundNumber);
        EventManager.RemoveHandler(GameEvent.OnUIGameOver,OnUIGameOver);
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

    void OnUIGameOver()
    {
        failPanel.gameObject.SetActive(true);
        failPanel.DOAnchorPos(Vector2.zero,0.5f);
    }

    
}
