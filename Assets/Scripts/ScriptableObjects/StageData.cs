using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "StageData", menuName = "Data/Stage"), InlineEditor]
[ShowOdinSerializedPropertiesInInspector]
public class StageData : ScriptableObject
{
    [BoxGroup("Stage Info"), LabelWidth(100)]
    public Dictionary<int, GameObject> StageInfo =
        new Dictionary<int, GameObject>();
    
}
