using UnityEngine;
using UnityEngine.UI;
using TMPro;

// this script is temporary, after the core implementation is done, this script would be altered based on it
// things to change: colors for selected, not selected one


public class SaveSlot : MonoBehaviour
{
    [ReadOnly, SerializeField] private Image imageBackgroundSelected;
    [ReadOnly, SerializeField] private TextMeshProUGUI textSaveName;
    [ReadOnly, SerializeField] private Color textColorSelected;
    [ReadOnly, SerializeField] private Color textColorNotSelected;

    private void Awake() {
        imageBackgroundSelected =  GetComponentsInChildren<Image>()[1];
        textSaveName =  GetComponentInChildren<TextMeshProUGUI>();

        textColorSelected = new Color(60f, 30f, 180f);
        textColorNotSelected = new Color(227f, 227f, 227f);
    }

    public void EnableSelected() {
        imageBackgroundSelected.gameObject.SetActive(true);
        textSaveName.color = textColorSelected;
    }

    public bool IsSelected() {
        return imageBackgroundSelected.gameObject.activeInHierarchy;
    }

    public void UpdateSaveName(string inputName) {
        textSaveName.text = inputName;
    }

    public void DisableSelected() {
        imageBackgroundSelected.gameObject.SetActive(false);
        textSaveName.color = textColorNotSelected;
    }
}