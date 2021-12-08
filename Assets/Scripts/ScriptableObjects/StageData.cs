using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "StageData", menuName = "Data/Stage")]
public class StageData : SerializedScriptableObject
{
    public Dictionary<GameStage, GameObject> data;
    public StageData()
    {
        data = new Dictionary<GameStage, GameObject>();
    }
    
}
