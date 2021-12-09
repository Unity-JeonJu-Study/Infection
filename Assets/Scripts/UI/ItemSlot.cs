using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    [ReadOnly, SerializeField] private Image imageItem;

    private void Awake() {
        imageItem =  GetComponentsInChildren<Image>()[1];
    }

    public void UpdateItemIcon(Sprite itemIcon) {
        imageItem.sprite = itemIcon;
    }
}