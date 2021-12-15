using UnityEngine;

public class PausePopup : MonoBehaviour
{
    public void OnClickResume() {
        GameManager.Instance.ResumeStageTimer();
        gameObject.SetActive(false);
    }

    public void OnClickMainMenu() {
        // scene manager part
        MySceneManager.instance.LoadScene("Main Menu Scene");

        Destroy(MySceneManager.instance.gameObject);
    }

    public void OnClickSetting() {
        PopupUIManager.instance.EnableSettingPopup();
    }
}