using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "DirectionData", menuName = "Data/DirectionData", order = 3)]
public class DirectionData : ScriptableObject 
{
    public string directionText;

    public int leftNumber;
    public int rightNumber;
    public int upNumber;
    public int downNumber;
}
