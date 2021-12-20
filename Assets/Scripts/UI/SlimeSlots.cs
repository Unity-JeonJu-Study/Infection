using UnityEngine;

public class SlimeSlots : MonoBehaviour
{
    [ReadOnly] public SlimeSlot [] slots;
    [ReadOnly, SerializeField] private int maxSlotsCount;
    [ReadOnly, SerializeField] private int curSlotsCount;

    private void Awake() {
        slots = GetComponentsInChildren<SlimeSlot>();
        maxSlotsCount = 10;
        curSlotsCount = 0;
    }

    public void ResetSlimeSlots() {
        curSlotsCount = 0;
        foreach(SlimeSlot curSlot in slots) {
            curSlot.gameObject.SetActive(true);
            curSlot.DisableRescuedSlimeSlot();
        }

        for(int curIndex = GameManager.Instance.stageData.data[GameManager.Instance.currentStage].maxSlimCount; curIndex < maxSlotsCount; curIndex++)
            slots[curIndex].gameObject.SetActive(false);
    }

    public void AddOneRescuedSlimeSlot() {
        if(curSlotsCount < 10) {
            slots[curSlotsCount].EnableRescuedSlimeSlot();
            curSlotsCount++;
        }
    }

    public int GetCurSlotsCount() {
        return curSlotsCount;
    }
}