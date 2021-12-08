using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainMenuPopup : MonoBehaviour
{
    public void OnClickStart() {
        // scene manager part
        //gameObject.SetActive(false);
    }

    public void OnClickLoad() {
        UIManager.instance.EnableLoadPopup();
    }

    public void OnClickExit() {
        #if UNITY_EDITOR
        EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}