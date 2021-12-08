using UnityEngine;

public class PausePopup : MonoBehaviour
{
    public void OnClickResume() {
        gameObject.SetActive(false);
    }

    public void OnClickMainMenu() {
        //UIManager.instance.EnableLoadPopup();
        // scene manager part
    }
}