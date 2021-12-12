using System;
using UnityEngine;

// this script is temporary, after the core implementation is done, this script would be altered based on it
// things to change: Game manager part

public class SavePopup : MonoBehaviour
{
    [ReadOnly, SerializeField] private SaveSlots saveSlots;

    [ReadOnly, SerializeField] private string tempSlotName;
    [ReadOnly, SerializeField] private int slotIndex;

    private void Awake() {
        saveSlots = GetComponentInChildren<SaveSlots>();

        slotIndex = 0;
    }

    private void Start() {
        for(; slotIndex < SaveLoadManager.instance.maxSaveSlot; slotIndex++) {
            DisableCurrentSaveSlot();
        }

        slotIndex = 0;
        EnableCurrentSaveSlot();
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.UpArrow)) 
            MoveCurrentSaveSlotUp();
        else if(Input.GetKeyDown(KeyCode.DownArrow)) 
            MoveCurrentSaveSlotDown();
        else if(Input.GetKeyDown(KeyCode.Return)) {
            SaveToCurrentSlot();
            LoadFiles();
        }
        else if(Input.GetKeyDown(KeyCode.Escape)) {
            // GameManager.instance.SetTimeScale(1f);
            // GameManager.instance.EnablePlayerInput();
            gameObject.SetActive(false);
        }
    }

    private void MoveCurrentSaveSlotUp() {
        DisableCurrentSaveSlot();
        slotIndex--;
        if(slotIndex < 0)
            slotIndex = SaveLoadManager.instance.maxSaveSlot-1;
        EnableCurrentSaveSlot();
    }
    private void MoveCurrentSaveSlotDown() {
        DisableCurrentSaveSlot();
        slotIndex++;
        if(slotIndex >= SaveLoadManager.instance.maxSaveSlot)
            slotIndex = 0;
        EnableCurrentSaveSlot();
    }

    private void DisableCurrentSaveSlot() {
        if(saveSlots.slots[slotIndex].IsSelected())
            saveSlots.slots[slotIndex].DisableSelected();
    }

    private void EnableCurrentSaveSlot() {
        if(saveSlots.slots[slotIndex].IsSelected() == false)
            saveSlots.slots[slotIndex].EnableSelected();
    }

    public void LoadFiles() {
        SaveLoadManager.instance.LoadAllData();

        for(int curIndex = 0; curIndex < SaveLoadManager.instance.maxSaveSlot; curIndex++) {
            saveSlots.slots[curIndex].UpdateSaveName(SaveLoadManager.instance.data[curIndex].GetSaveSlotName());
        }
    }
    private void SaveToCurrentSlot() {
        Debug.Log("Save worked");
        SaveLoadManager.instance.SaveData(slotIndex, new SaveData(-1));

        // SavePoint targetSavePoint = GameManager.instance.player.playerInteraction.targetSavePoint;
        // SaveLoadManager.instance.SaveData(slotIndex, new SaveData(MySceneManager.instance.curSceneName, DateTime.Now, targetSavePoint.respawnPoint, GameManager.instance.player.playerInfo.availableWeapons, GameManager.instance.player.playerInfo.health));
    }
}