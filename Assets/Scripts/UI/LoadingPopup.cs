using UnityEngine;
using Michsky.UI.ModernUIPack;

public class LoadingPopup : MonoBehaviour
{
    public ProgressBar progressBar;

    private void Awake() {
        LoadingPopup[] objects = FindObjectsOfType<LoadingPopup>();
        if(objects.Length > 1)
            Destroy(gameObject);
        else {
            progressBar = GetComponentInChildren<ProgressBar>();

            DontDestroyOnLoad(gameObject);
        }
    }
}
