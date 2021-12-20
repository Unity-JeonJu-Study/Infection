using System;
using System.Collections.Generic;
using System.IO;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "StageData", menuName = "Data/Stage")]
public class StageData : SerializedScriptableObject
{
    public Dictionary<GameStage, Stage> data = new Dictionary<GameStage, Stage>();

    [Button("Generate Stage")]
    private void GenerateStage()
    {
        foreach (GameStage gameStage in Enum.GetValues(typeof(GameStage)))
        {
            if (!data.ContainsKey(gameStage))
                data.Add(gameStage, new Stage());
            if (File.Exists("Assets/Resources/Data/Level/" + gameStage + ".prefab"))
                data[gameStage].gamePrefab = Resources.Load<GameObject>("Data/Level/" + gameStage);
            if (File.Exists("Assets/Resources/Data/Quest/_" + gameStage + ".asset"))
                data[gameStage].quest = Resources.Load<QuestList>("Data/Quest/_" + gameStage);
            if (File.Exists("Assets/Resources/Sound Player/BGM/" + gameStage + ".wav"))
                data[gameStage].bgm = Resources.Load<AudioClip>("Sound Player/BGM/" + gameStage).name;
        }
    }

    public class Stage
    {
        public GameObject gamePrefab;
        public QuestList quest;
        public string bgm;
        public float limitTime;
        public int minSlimeCount;
        public int maxSlimeCount;
    }
}