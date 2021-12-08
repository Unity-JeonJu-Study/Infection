using System;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    [ReadOnly] public static SaveLoadManager instance;

    [ReadOnly] public List<SaveData> data;
    [ReadOnly] public int maxSaveSlot;

    [ReadOnly, SerializeField] private string defaultSavePath;

    private void Awake() {
        instance = this;

        defaultSavePath = "SaveData/";
        data = new List<SaveData>();
        maxSaveSlot = 5;

        LoadAllData();
    }

    public void LoadAllData() {
        data.Clear();

        for(int curIndex = 0; curIndex < maxSaveSlot; curIndex++) {
            if(ES3.KeyExists(curIndex.ToString(), defaultSavePath + curIndex + ".es3"))
                data.Add(ES3.Load<SaveData>(curIndex.ToString(), defaultSavePath + curIndex + ".es3"));
            else
                data.Add(new SaveData(true));
        }
    }

    public SaveData GetLatestData() {
        int latestDataIndex = 0;
        for(int curIndex = 1; curIndex < maxSaveSlot; curIndex++) {
            if(DateTime.Compare(data[latestDataIndex].saveDateTime, data[curIndex].saveDateTime) < 0) {
                latestDataIndex = curIndex;
            }
        }
        return data[latestDataIndex];
    }

    public void SaveData(int slotIndex, SaveData inputData) {
        ES3.Save(slotIndex.ToString(), inputData, defaultSavePath + slotIndex + ".es3");
    }
}
