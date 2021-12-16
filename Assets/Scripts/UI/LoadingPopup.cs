using UnityEngine;

public class LoadingPopup : MonoBehaviour
{
    private void Awake() {
        MySceneManager.instance.UpdateLoadingPopup(this);
    }
}
