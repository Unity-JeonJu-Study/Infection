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
    private WaitForSeconds waitTimeForLoading;
    private int curProgress;


    private void Awake() {
        MySceneManager[] objects = FindObjectsOfType<MySceneManager>();
        if(objects.Length > 1)
            Destroy(gameObject);
        else {
            instance = this;
            curSceneName = "Main Menu Scene";
            isInitial = true;

            waitTimeForLoading = new WaitForSeconds(0.01f);

            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start() {
        loadingPopup = FindObjectOfType<LoadingPopup>();
        DisableLoadingPopup();
        isLoadingPopupDisabled = true;
    }

    public void LoadScene(string sceneName, bool isRelatedToMainMenu = true) {
        curSceneName = sceneName;

        if(isRelatedToMainMenu)
            StartCoroutine(LoadAsynchronously(sceneName));
        else
            ShowLoadingPopupForTwoSeconds();
    }

    private IEnumerator LoadAsynchronously(string sceneName) {
        EnableLoadingPopup();

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);

        while(!operation.isDone) {
            loadingPopup.progressBar.currentPercent = operation.progress * 100f;
            yield return waitTimeForLoading;
        }

        DisableLoadingPopup();
    }

    private IEnumerator ShowLoadingPopupForTwoSeconds() {
        EnableLoadingPopup();

        curProgress = 0;
        while(curProgress < 100) {
            loadingPopup.progressBar.currentPercent = curProgress;
            yield return waitTimeForLoading;
            curProgress++;
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