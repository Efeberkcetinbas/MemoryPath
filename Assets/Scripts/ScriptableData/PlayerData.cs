using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "PlayerData", menuName = "Data/PlayerData", order = 1)]
public class PlayerData : ScriptableObject 
{
    public bool playerCanMove=true;
    public bool playerUp=true;
    public bool playerDown=true;
    public bool playerLeft=true;
    public bool playerRight=true;
    
}
