using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainMenu : MonoBehaviour
{
    [ReadOnly, SerializeField] private LoadPopup popupLoad;
    [ReadOnly, SerializeField] private SettingPopup popupSetting;

    private void Awake() {
        popupLoad = FindObjectOfType<LoadPopup>();
        popupLoad.gameObject.SetActive(false);

        popupSetting = FindObjectOfType<SettingPopup>();
        popupSetting.gameObject.SetActive(false);
    }

    public void OnClickStart() {
        // scene manager part
        MySceneManager.instance.LoadScene("Main Play Scene");
    }

    public void OnClickLoad() {
        popupLoad.gameObject.SetActive(true);
        popupLoad.LoadFiles();
    }

    public void OnClickSetting() {
        popupSetting.gameObject.SetActive(true);
    }

    public void OnClickExit() {
        #if UNITY_EDITOR
        EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}