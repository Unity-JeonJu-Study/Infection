using System.Collections.Generic;
using System.IO;
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
        public QuestList quest;

        [Button("Load Quest")]
        private void LoadQuest()
        {
            if (File.Exists("Assets/Resources/Data/Quest/_" + gamePrefab.name + ".asset"))
                quest = Resources.Load<QuestList>("Data/Quest/" + gamePrefab.name);
            else 
                Debug.LogError("File does not exist");
        }
    }
}
