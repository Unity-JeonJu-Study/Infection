using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "StageData", menuName = "Data/Stage")]
public class StageData : SerializedScriptableObject
{
    public Dictionary<GameStage, Stage> data;
    public StageData()
    {
        data = new Dictionary<GameStage, Stage>();
    }

    public class Stage
    {
        public GameObject gamePrefab;
        public float limitTime;
    }
}
