using UnityEngine;
using TMPro;

public class StageClearPopup : MonoBehaviour
{
    [ReadOnly, SerializeField] private TextMeshProUGUI textClearInfo;

    private void Awake() {
        textClearInfo = GetComponentsInChildren<TextMeshProUGUI>()[1];
    }

    public void UpdateClearInfo(int curCount, int maxCount) {
        textClearInfo.text = "Rescued Slimes: " + curCount + " / " + maxCount;
    }
}