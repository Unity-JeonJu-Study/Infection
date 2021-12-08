using UnityEngine;
using UnityEngine.SceneManagement;

// this script is temporary, after the core implementation is done, this script would be altered based on it
// things to change: cur scene name
public class MySceneManager : MonoBehaviour
{
    [ReadOnly] public static MySceneManager instance;
    [ReadOnly] public SaveData loadedData;

    [ReadOnly] public string curSceneName;

    private void Awake() {
        instance = this;
        curSceneName = "Temp Main Menu";

        DontDestroyOnLoad(gameObject);
    }

    public void LoadScene(string sceneName) {
        curSceneName = sceneName;
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}