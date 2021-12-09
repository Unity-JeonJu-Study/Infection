using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InGameUIManager : MonoBehaviour
{
    [ReadOnly] public static InGameUIManager instance;

    [ReadOnly, SerializeField] private TextMeshProUGUI textObjective;
    [ReadOnly, SerializeField] private TextMeshProUGUI textTime;
    [ReadOnly, SerializeField] private ItemSlots itemSlots;

    private void Awake() {
        instance = this;

        TextMeshProUGUI[] texts = GetComponentsInChildren<TextMeshProUGUI>();        
        textObjective = texts[0];
        textTime = texts[1];

        itemSlots = GetComponentInChildren<ItemSlots>();
    }

    private void Update() {
        // this code exists for debug purpose only
        
        // if(Input.GetKeyDown(KeyCode.Escape))
        //     EnablePausePopup();

        // if(Input.GetKeyDown(KeyCode.K))
        //     EnableGameOverPopup();
    }

    
}