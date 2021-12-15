using System;
using System.Collections.Generic;
using UnityEngine;

// this script is temporary, after the core implementation is done, this script would be altered based on it
// things to change: everything -> proper data related to this project should be here

[Serializable]
public struct SaveData
{
    public bool isLoaded;
    public string stageName;
    public DateTime saveDateTime;
    public Vector3 respawnPoint;
    //public List<AvailableWeapon> savedAvailableWeapons;
    public int savedHealth;

    public SaveData(int temp) {
        isLoaded = true;

        stageName = "temp stage";
        saveDateTime = DateTime.Now;
        respawnPoint = Vector3.one;
        //savedAvailableWeapons = null;
        savedHealth = -1;
    }

    public SaveData(GameStage currentStage, DateTime inputSaveDateTime, Vector3 inputRespawnPoint, /*List<AvailableWeapon> inputSavedAvailableWeapon,*/ int inputSavedHealth) {
        isLoaded = true;
        
        switch(currentStage) {
            case GameStage.Stage1: stageName = "Stage1"; break;
            case GameStage.Stage2: stageName = "Stage2"; break;
            case GameStage.Stage3: stageName = "Stage3"; break;
            default: stageName = "Laboratory"; break;
        }
        saveDateTime = inputSaveDateTime;
        respawnPoint = inputRespawnPoint;

        //savedAvailableWeapons = inputSavedAvailableWeapon;
        savedHealth = inputSavedHealth;
    }

    public SaveData(bool trashValue) {
        isLoaded = false;

        stageName = "";
        saveDateTime = DateTime.MinValue;
        respawnPoint = Vector3.one;
        //savedAvailableWeapons = null;
        savedHealth = -1;
    }

    public string GetSaveSlotName() {
        if(isLoaded) 
            return stageName + " " + saveDateTime.ToString("yyyy/mm/dd  hh/mm/ss");
        else 
            return "empty";
    }
}
