using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;


public class QuestData
{
    public Dictionary<string, QuestList> Quests = new Dictionary<string, QuestList>();
}

[CreateAssetMenu(fileName = "QuestData", menuName = "Data/Quest")]
public class QuestList : SerializedScriptableObject
{
    public string flag;
    public Queue<Quest> questList;

    public QuestList()
    {
        this.flag = "default";
        questList = new Queue<Quest>();
    }
    
    public QuestList(string flag)
    {
        this.flag = flag;
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