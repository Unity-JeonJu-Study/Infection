using UnityEngine;

public class SaveSlots : MonoBehaviour
{
    [ReadOnly] public SaveSlot [] slots;

    private void Awake() {
        slots = GetComponentsInChildren<SaveSlot>();
    }
}