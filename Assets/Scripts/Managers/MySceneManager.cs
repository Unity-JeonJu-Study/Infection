using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour
{
    [ReadOnly] public static MySceneManager instance;
    [ReadOnly] public SaveData loadedData;

    [ReadOnly] public string curSceneName;
    [ReadOnly] public bool isInitial;
    [ReadOnly, SerializeField] private LoadingPopup loadingPopup;

    private void Awake() {
        instance = this;
        curSceneName = "Main Menu Scene";
        isInitial = true;
        loadingPopup = FindObjectOfType<LoadingPopup>();
        DisableLoadingPopup();

        DontDestroyOnLoad(gameObject);
    }

    public void LoadScene(string sceneName) {
        curSceneName = sceneName;
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    public void LoadCutScene(string sceneName)
    {
        curSceneName = sceneName;
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }

    public void UnLoadCutScene()
    {
        SceneManager.UnloadSceneAsync(curSceneName);
    }

    public void UpdateLoadingPopup(LoadingPopup inputLoadingPopup) {
        loadingPopup = inputLoadingPopup;
    }

    public void EnableLoadingPopup() {
        loadingPopup.gameObject.SetActive(true);
    }

    public void DisableLoadingPopup() {
        loadingPopup.gameObject.SetActive(false);
    }
}