using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this script is temporary, after the core implementation is done, this script would be altered based on it

public class UIManager : MonoBehaviour
{
    [ReadOnly] public static UIManager instance;

    [ReadOnly, SerializeField] private SavePopup popupSave;

    private void Awake() {
        instance = this;
    }

    private void Update() {
        
    }
}
