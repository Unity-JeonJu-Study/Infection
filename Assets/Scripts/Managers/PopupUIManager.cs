using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this script is temporary, after the core implementation is done, this script would be altered based on it
// things to change: other popup object would be added

public class PopupUIManager : MonoBehaviour
{
    [ReadOnly] public static PopupUIManager instance;

    [ReadOnly, SerializeField] private SavePopup popupSave;
    [ReadOnly, SerializeField] private PausePopup popupPause;
    [ReadOnly, SerializeField] private GameOverPopup popupGameOver;

    private void Awake() {
        instance = this;

        popupSave = FindObjectOfType<SavePopup>();
        popupSave.gameObject.SetActive(false);

        popupPause = FindObjectOfType<PausePopup>();
        popupPause.gameObject.SetActive(false);

        popupGameOver = FindObjectOfType<GameOverPopup>();
        popupGameOver.gameObject.SetActive(false);
    }

    private void Update() {
        // this code exists for debug purpose only
        
        // if(Input.GetKeyDown(KeyCode.Escape))
        //     EnablePausePopup();

        // if(Input.GetKeyDown(KeyCode.K))
        //     EnableGameOverPopup();
    }

    public void EnableSavePopup() {
        popupSave.gameObject.SetActive(true);
        popupSave.LoadFiles();
    }

    public void EnablePausePopup() {
        popupPause.gameObject.SetActive(true);
    }

    public void EnableGameOverPopup() {
        popupGameOver.gameObject.SetActive(true);
    }
}
