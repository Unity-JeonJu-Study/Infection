using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this script is temporary, after the core implementation is done, this script would be altered based on it
// things to change: other popup object would be added

public class PopupUIManager : MonoBehaviour
{
    [ReadOnly] public static PopupUIManager instance;

    [ReadOnly, SerializeField] private TutorialPopup popupTutorial;
    [ReadOnly, SerializeField] private SavePopup popupSave;
    [ReadOnly, SerializeField] private PausePopup popupPause;
    [ReadOnly, SerializeField] private SettingPopup popupSetting;
    [ReadOnly, SerializeField] private GameOverPopup popupGameOver;

    private void Awake() {
        instance = this;
    }

    private void Start() {
        popupTutorial = GetComponentInChildren<TutorialPopup>();
        popupTutorial.gameObject.SetActive(false);

        popupSave = GetComponentInChildren<SavePopup>();
        popupSave.gameObject.SetActive(false);

        popupPause = GetComponentInChildren<PausePopup>();
        popupPause.gameObject.SetActive(false);
        
        popupSetting = GetComponentInChildren<SettingPopup>();
        popupSetting.gameObject.SetActive(false);

        popupGameOver = GetComponentInChildren<GameOverPopup>();
        popupGameOver.gameObject.SetActive(false);
    }

    private void Update() {
        // this code exists for debug purpose only
        if(Input.GetKeyDown(KeyCode.Escape))
            EnablePausePopup();
        if(Input.GetKeyDown(KeyCode.K))
            EnableSettingPopup();
    }

    public void EnableTutorialPopup() {
        GameManager.Instance.PauseStageTimer();
        popupTutorial.gameObject.SetActive(true);
    }

    public void EnableSavePopup() {
        popupSave.gameObject.SetActive(true);
        popupSave.LoadFiles();
    }

    public void EnablePausePopup() {
        GameManager.Instance.PauseStageTimer();
        popupPause.gameObject.SetActive(true);
    }

    public void EnableSettingPopup() {
        popupSetting.gameObject.SetActive(true);
    }

    public void EnableGameOverPopup() {
        popupGameOver.gameObject.SetActive(true);
    }
}
