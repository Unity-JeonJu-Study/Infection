using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "StageData", menuName = "Data/Stage"), InlineEditor]
[ShowOdinSerializedPropertiesInInspector]
public class StageData : ScriptableObject
{
    [BoxGroup("Stage Info"), LabelWidth(100)]
    public int stageNum;
    [BoxGroup("Stage Info"), LabelWidth(100)]
    public GameObject stagePrefab;
}
