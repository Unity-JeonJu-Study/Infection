using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

// this script is temporary, after the core implementation is done, this script would be altered based on it
// things to change: start method

public class MainMenuPopup : MonoBehaviour
{
    [ReadOnly, SerializeField] private LoadPopup popupLoad;

    private void Awake() {
        popupLoad = FindObjectOfType<LoadPopup>();
    }

    public void OnClickStart() {
        // scene manager part
        MySceneManager.instance.LoadScene("Choi Kang In");
    }

    public void OnClickLoad() {
        popupLoad.gameObject.SetActive(true);
        popupLoad.LoadFiles();
    }

    public void OnClickExit() {
        #if UNITY_EDITOR
        EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}