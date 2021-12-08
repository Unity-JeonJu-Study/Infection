using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this script is temporary, after the core implementation is done, this script would be altered based on it
// things to change: other popup object would be added


public class UIManager : MonoBehaviour
{
    [ReadOnly] public static UIManager instance;

    [ReadOnly, SerializeField] private SavePopup popupSave;

    private void Awake() {
        instance = this;

        popupSave = FindObjectOfType<SavePopup>();
        popupSave.gameObject.SetActive(false);
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.K))
            EnableSavePopup();
    }

    public void EnableSavePopup() {
        // GameManager.instance.DisablePlayerInput();
        // GameManager.instance.SetTimeScale(0.01f);
        popupSave.gameObject.SetActive(true);
        popupSave.LoadFiles();
    }
}
