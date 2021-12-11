using UnityEngine;

public class SlimSlots : MonoBehaviour
{
    [ReadOnly] public SlimSlot [] slots;
    [ReadOnly, SerializeField] private int maxSlotsCount;
    [ReadOnly, SerializeField] private int curSlotsCount;

    private void Awake() {
        slots = GetComponentsInChildren<SlimSlot>();
        maxSlotsCount = 10;
        curSlotsCount = 0;
    }

    public void ResetSlimSlots() {
        curSlotsCount = 0;
        foreach(SlimSlot curSlot in slots) {
            curSlot.gameObject.SetActive(true);
            curSlot.DisableRescuedSlimSlot();
        }

        for(int curIndex = GameManager.Instance.stageData.data[GameManager.Instance.currentStage].maxSlimCount; curIndex < maxSlotsCount; curIndex++)
            slots[curIndex].gameObject.SetActive(false);
    }

    public void AddOneRescuedSlimSlot() {
        if(curSlotsCount < 10) {
            slots[curSlotsCount].EnableRescuedSlimSlot();
            curSlotsCount++;
        }
    }
}