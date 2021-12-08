using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this script is temporary, after the core implementation is done, this script would be altered based on it
// things to change: other popup object would be added

public class UIManager : MonoBehaviour
{
    [ReadOnly] public static UIManager instance;

    [ReadOnly, SerializeField] private MainMenuPopup popupMainMenu;
    [ReadOnly, SerializeField] private SavePopup popupSave;
    [ReadOnly, SerializeField] private LoadPopup popupLoad;

    private void Awake() {
        instance = this;

        popupMainMenu = FindObjectOfType<MainMenuPopup>();

        popupSave = FindObjectOfType<SavePopup>();
        popupSave.gameObject.SetActive(false);

        popupLoad = FindObjectOfType<LoadPopup>();
        popupLoad.gameObject.SetActive(false);
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.K))
            EnableMainMenuPopup();
    }

    public void EnableMainMenuPopup() {
        popupMainMenu.gameObject.SetActive(true);
    }

    public void EnableSavePopup() {
        // GameManager.instance.DisablePlayerInput();
        // GameManager.instance.SetTimeScale(0.01f);
        popupSave.gameObject.SetActive(true);
        popupSave.LoadFiles();
    }

    public void EnableLoadPopup() {
        popupLoad.gameObject.SetActive(true);
        popupLoad.LoadFiles();
    }
}
