using UnityEngine;

public class ItemSlots : MonoBehaviour
{
    [ReadOnly] public ItemSlot [] slots;

    private void Awake() {
        slots = GetComponentsInChildren<ItemSlot>();
    }
}