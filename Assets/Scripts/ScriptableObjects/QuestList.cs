using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestList", menuName = "Data/Quest")]
public class QuestList : SerializedScriptableObject
{
    public GameStage stage;
    public Queue<Quest> questList;

    public QuestList()
    {
        this.stage = default;
        questList = new Queue<Quest>();
    }
    
    public QuestList(GameStage stage)
    {
        this.stage = stage;
        questList = new Queue<Quest>();
    }
}

public class Quest
{
    [BoxGroup("Quest Info"), LabelWidth(100)]
    public string questName;
    [BoxGroup("Quest Info"), LabelWidth(100), TextArea]
    public string description;
    [BoxGroup("Quest Info"), LabelWidth(100)]
    public bool isComplete;

    public Quest()
    {
        questName = "Enter name";
        description = "Enter description";
        isComplete = false;
    }

    public Quest(string name,
        string description,
        bool isComplete = false)
    {
        questName = name;
        this.description = description;
        this.isComplete = isComplete;
    }

}
