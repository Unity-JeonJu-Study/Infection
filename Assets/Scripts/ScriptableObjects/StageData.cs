using System;
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

    [Button("Generate Stage")]
    private void GenerateStage()
    {
        foreach (GameStage gameStage in Enum.GetValues(typeof(GameStage)))
        {
            if (!data.ContainsKey(gameStage))
                data.Add(gameStage, new Stage());

            data[gameStage].gamePrefab = Resources.Load<GameObject>("Data/Level/" + gameStage);
            data[gameStage].quest = Resources.Load<QuestList>("Data/Quest/_" + gameStage);
            data[gameStage].bgm = Resources.Load("Sound Player/BGM/" + gameStage).ToString();
        }


        // foreach (var stage in data)
        // {
        //     stage.Value.gamePrefab = Resources.Load<GameObject>("Data/Level/" + stage.Key);
        //     stage.Value.quest = Resources.Load<QuestList>("Data/Quest/_" +  stage.Key);
        //     stage.Value.bgm = Resources.Load("Sound Player/BGM/" +  stage.Key).ToString();
        // }
    }

    public class Stage
    {
        public GameObject gamePrefab;
        public QuestList quest;
        public string bgm;
        public float limitTime;
        public int minSlimCount;
        public int maxSlimCount;

        [Button("Load Quest")]
        private void LoadQuest()
        {
            if (File.Exists("Assets/Resources/Data/Quest/_" + gamePrefab.name + ".asset"))
                quest = Resources.Load<QuestList>("Data/Quest/_" + gamePrefab.name);
            else
                Debug.LogError("File does not exist");
        }
    }
}