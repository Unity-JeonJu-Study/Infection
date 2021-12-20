using UnityEngine;
using UnityEngine.UI;

public class SlimeSlot : MonoBehaviour
{
    [ReadOnly, SerializeField] private Image imageRescuedSlime;

    private void Awake() {
        imageRescuedSlime =  GetComponentsInChildren<Image>()[1];
    }

    public void EnableRescuedSlimeSlot() {
        imageRescuedSlime.enabled = true;
    }

    public void DisableRescuedSlimeSlot() {
        imageRescuedSlime.enabled = false;
    }
}