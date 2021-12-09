using UnityEngine;

// this script is temporary, after the core implementation is done, this script would be altered based on it
// things to change: scene name

public class PausePopup : MonoBehaviour
{
    public void OnClickResume() {
        gameObject.SetActive(false);
    }

    public void OnClickMainMenu() {
        // scene manager part
        MySceneManager.instance.LoadScene("Temp Main Menu");

        Destroy(MySceneManager.instance.gameObject);
        Destroy(SaveLoadManager.instance.gameObject);
    }
}