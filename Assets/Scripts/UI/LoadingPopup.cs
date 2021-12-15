using UnityEngine;

public class LoadingPopup : MonoBehaviour
{
    public static LoadingPopup instance;

    private void Awake() {
        instance = this;

        DontDestroyOnLoad(gameObject);
    }
}
