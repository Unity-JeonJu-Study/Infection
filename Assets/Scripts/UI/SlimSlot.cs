using UnityEngine;
using UnityEngine.UI;

public class SlimSlot : MonoBehaviour
{
    [ReadOnly, SerializeField] private Image imageRescuedSlim;

    private void Awake() {
        imageRescuedSlim =  GetComponentsInChildren<Image>()[1];
    }

    public void EnableRescuedSlimSlot() {
        imageRescuedSlim.enabled = true;
    }

    public void DisableRescuedSlimSlot() {
        imageRescuedSlim.enabled = false;
    }
}