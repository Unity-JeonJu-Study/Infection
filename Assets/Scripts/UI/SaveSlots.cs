using UnityEngine;

public class SaveSlots : MonoBehaviour
{
    public SaveSlot [] slots;

    private void Awake() {
        slots = GetComponentsInChildren<SaveSlot>();
    }
}