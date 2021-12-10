using System;
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

    [ReadOnly] public List<Sprite> itemIcons;

    private Vector2 vectorCenter;
    private WaitForSeconds waitTime;
    private WaitForSeconds waitTwoSeconds;
    
    private int minute, second;
    private int curSlotIndex;

    private void Awake() {
        instance = this;

        TextMeshProUGUI[] texts = GetComponentsInChildren<TextMeshProUGUI>();        
        textObjective = texts[0];
        textTime = texts[1];

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
        //     UpdateObjectiveText("This is new objective");
    }

    public void UpdateObjectiveText(string newObjective) {
        textObjective.text = newObjective;


        textObjective.rectTransform.pivot = vectorCenter;
        textObjective.rectTransform.anchorMin = vectorCenter;
        textObjective.rectTransform.anchorMax = vectorCenter;
        textObjective.fontSize = 100;
        textObjective.alignment = TextAlignmentOptions.Center;

        StartCoroutine("MoveObjectiveText");
    }   

    private IEnumerator MoveObjectiveText() {
        yield return waitTwoSeconds;

        for(Vector2 vectorTemp = textObjective.rectTransform.pivot; textObjective.rectTransform.pivot.x > 0.1; vectorTemp.x -= 0.01f, vectorTemp.y += 0.01f) {
            textObjective.rectTransform.pivot = vectorTemp;
            textObjective.rectTransform.anchorMin = vectorTemp;
            textObjective.rectTransform.anchorMax = vectorTemp;

            textObjective.fontSize -= 1;

            yield return waitTime;
        }

        FinalizeObjectiveText(); 
    }

    private void FinalizeObjectiveText() {
        textObjective.rectTransform.pivot = Vector2.up;
        textObjective.rectTransform.anchorMin = Vector2.up;
        textObjective.rectTransform.anchorMax = Vector2.up;
        textObjective.alignment = TextAlignmentOptions.Left;

        textObjective.fontSize = 36;
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