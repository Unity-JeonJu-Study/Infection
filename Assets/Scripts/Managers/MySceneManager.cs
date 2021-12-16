using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour
{
    [ReadOnly] public static MySceneManager instance;
    [ReadOnly] public SaveData loadedData;

    [ReadOnly] public string curSceneName;
    [ReadOnly] public bool isInitial;
    [ReadOnly, SerializeField] private LoadingPopup loadingPopup;
    [ReadOnly, SerializeField] private bool isLoadingPopupDisabled;

    private void Awake() {
        MySceneManager[] objects = FindObjectsOfType<MySceneManager>();
        if(objects.Length > 1)
            Destroy(gameObject);
        else {
            instance = this;
            curSceneName = "Main Menu Scene";
            isInitial = true;

            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start() {
        loadingPopup = FindObjectOfType<LoadingPopup>();
        DisableLoadingPopup();
        isLoadingPopupDisabled = true;
    }

    public void LoadScene(string sceneName) {
        curSceneName = sceneName;
        EnableLoadingPopup();
        StartCoroutine(LoadAsynchronously(sceneName));
    }

    private IEnumerator LoadAsynchronously(string sceneName) {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);

        while(!operation.isDone) {
            loadingPopup.progressBar.currentPercent = operation.progress * 100f;
            yield return null;
        }

        DisableLoadingPopup();
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

    public void EnableLoadingPopup() {
        if(isLoadingPopupDisabled) {
            loadingPopup.gameObject.SetActive(true);
            isLoadingPopupDisabled = false;
        }
    }

    public void DisableLoadingPopup() {
        if(!isLoadingPopupDisabled) {
            loadingPopup.gameObject.SetActive(false);
            isLoadingPopupDisabled = true;
        }
    }
}