using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

// this script is temporary, after the core implementation is done, this script would be altered based on it
// things to change: scene name

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
        MySceneManager.instance.LoadScene("Choi Kang In");
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