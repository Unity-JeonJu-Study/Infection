using UnityEngine;
using UnityEngine.UI;
using TMPro;

// this script is temporary, after the core implementation is done, this script would be altered based on it
// things to change: colors for selected, not selected one


public class SaveSlot : MonoBehaviour
{
    [ReadOnly, SerializeField] private Image imageBackgroundSelected;
    [ReadOnly, SerializeField] private TextMeshProUGUI textSaveSlot;
    [ReadOnly, SerializeField] private TextMeshProUGUI textSaveName;
    [ReadOnly, SerializeField] private Color textColorSelected;
    [ReadOnly, SerializeField] private Color textColorNotSelected;

    private void Awake() {
        imageBackgroundSelected =  GetComponentsInChildren<Image>()[1];
        TextMeshProUGUI[] texts = GetComponentsInChildren<TextMeshProUGUI>();
        textSaveSlot = texts[0];
        textSaveName =  texts[1];

        textColorSelected = new Color(1, 1, 1);
        textColorNotSelected = new Color(0.18f, 0.68f, 0.33f);
    }

    public void EnableSelected() {
        textSaveSlot.color = textColorSelected;
        textSaveName.color = textColorSelected;

        imageBackgroundSelected.gameObject.SetActive(true);
    }

    public bool IsSelected() {
        return imageBackgroundSelected.gameObject.activeInHierarchy;
    }

    public void UpdateSaveName(string inputName) {
        textSaveName.text = inputName;
    }

    public void DisableSelected() {
        textSaveSlot.color = textColorNotSelected;
        textSaveName.color = textColorNotSelected;

        imageBackgroundSelected.gameObject.SetActive(false);
    }
}