using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InGameUIManager : MonoBehaviour
{
    [ReadOnly] public static InGameUIManager instance;

    [ReadOnly, SerializeField] private TextMeshProUGUI textMainObjective;
    [ReadOnly, SerializeField] private TextMeshProUGUI textSubObjective;
    [ReadOnly, SerializeField] private SlimSlots slimSlots;
    [ReadOnly, SerializeField] private TextMeshProUGUI textTime;
    [ReadOnly, SerializeField] private ItemSlots itemSlots;

    [ReadOnly] public List<Sprite> itemIcons;

    private Vector2 vectorCenter;
    private WaitForSeconds waitTime;
    private WaitForSeconds waitTwoSeconds;
    
    private int minute, second;
    private int curSlotIndex;

    private void Awake() {
        instance = this;

        TextMeshProUGUI[] texts = GetComponentsInChildren<TextMeshProUGUI>();        
        textMainObjective = texts[0];
        textSubObjective = texts[1];
        textTime = texts[2];

        slimSlots = GetComponentInChildren<SlimSlots>();
        itemSlots = GetComponentInChildren<ItemSlots>();


        // the order of loading is based on the Item enum type
        itemIcons = new List<Sprite>();
        // itemIcons.Add(Resources.Load<Sprite>("UIItemIcons/itemName"));


        vectorCenter = new Vector2(0.5f, 0.5f);
        waitTime = new WaitForSeconds(0.01f);
        waitTwoSeconds = new WaitForSeconds(2f);
    }

    private void Update() {
        // this code exists for debug purpose only
        // if(Input.GetKeyDown(KeyCode.Escape))
        //     PopupUIManager.instance.EnablePausePopup();
        // if(Input.GetKeyDown(KeyCode.K))
        //     ResetSlimSlots();

        // if(Input.GetKeyDown(KeyCode.L))
        //     AddOneRescuedSlimSlot();
    }

    public void DisableAllInGameUIs() {
        gameObject.SetActive(false);
    }

    public void EnableAllInGameUIs() {
        gameObject.SetActive(true);
    }

    public void UpdateObjectiveText(string newObjective, bool isSubObjective) {
        TextMeshProUGUI textObjective;
        if(isSubObjective)
            textObjective = textSubObjective;
        else
            textObjective = textMainObjective;


        textObjective.text = newObjective;

        textObjective.rectTransform.pivot = vectorCenter;
        textObjective.rectTransform.anchorMin = vectorCenter;
        textObjective.rectTransform.anchorMax = vectorCenter;
        textObjective.fontSize = 100;
        textObjective.alignment = TextAlignmentOptions.Center;

        StartCoroutine(MoveObjectiveText(isSubObjective));
    }   

    private IEnumerator MoveObjectiveText(bool isSubObjective) {
        TextMeshProUGUI textObjective;
        if(isSubObjective)
            textObjective = textSubObjective;
        else
            textObjective = textMainObjective;

        yield return waitTwoSeconds;

        for(Vector2 vectorTemp = textObjective.rectTransform.pivot; textObjective.rectTransform.pivot.x > 0.1; vectorTemp.x -= 0.01f, vectorTemp.y += 0.01f) {
            textObjective.rectTransform.pivot = vectorTemp;
            textObjective.rectTransform.anchorMin = vectorTemp;
            textObjective.rectTransform.anchorMax = vectorTemp;

            textObjective.fontSize -= 1;

            yield return waitTime;
        }

        FinalizeObjectiveText(isSubObjective); 
    }

    private void FinalizeObjectiveText(bool isSubObjective) {
        TextMeshProUGUI textObjective;
        if(isSubObjective)
            textObjective = textSubObjective;
        else
            textObjective = textMainObjective;


        textObjective.rectTransform.pivot = Vector2.up;
        textObjective.rectTransform.anchorMin = Vector2.up;
        textObjective.rectTransform.anchorMax = Vector2.up;
        textObjective.alignment = TextAlignmentOptions.Center;


        if(isSubObjective)
            textObjective.fontSize = 70;
        else
            textObjective.fontSize = 36;
    }

    public void ResetSlimSlots() {
        slimSlots.ResetSlimSlots();
    }

    public void AddOneRescuedSlimSlot() {
        slimSlots.AddOneRescuedSlimSlot();
    }

    public void EnableTimeText() {
        textTime.gameObject.SetActive(true);
    }
    public void DisableTimeText() {
        textTime.gameObject.SetActive(false);
    }

    public void UpdateTimeText(int time) {
        minute = time / 60;
        second = time % 60;
        if(minute == 0)
            textTime.text = "00:";
        else {
            if(minute < 10)
                textTime.text = "0" + minute + ":";
            else
                textTime.text = minute + ":";
        }

        if(second < 10)
            textTime.text += "0" + second;
        else
            textTime.text += second;
    }

    public void UpdateItemIcons() {
        for(curSlotIndex = 0; curSlotIndex < 3; curSlotIndex++) 
            itemSlots.slots[curSlotIndex].gameObject.SetActive(false);

        curSlotIndex = 0;
        foreach(Item curItem in GameManager.Instance.player.inventory) {
            itemSlots.slots[curSlotIndex].gameObject.SetActive(true);
            itemSlots.slots[curSlotIndex].UpdateItemIcon(itemIcons[(int)curItem]);

            curSlotIndex++;
        }
    }
}