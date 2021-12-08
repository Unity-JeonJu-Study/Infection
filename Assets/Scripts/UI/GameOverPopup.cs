using UnityEngine;

// this script is temporary, after the core implementation is done, this script would be altered based on it
// things to change: scene names

public class GameOverPopup : MonoBehaviour
{
    public void OnClickRestart() {
        // restart
        MySceneManager.instance.loadedData = SaveLoadManager.instance.GetLatestData();
        if(MySceneManager.instance.loadedData.isLoaded)
            MySceneManager.instance.isInitial = false;
        else
            MySceneManager.instance.isInitial = true;

        MySceneManager.instance.LoadScene("Choi Kang In");
    }

    public void OnClickMainMenu() {
        // scene manager part
        MySceneManager.instance.LoadScene("Temp Main Menu");

        Destroy(MySceneManager.instance.gameObject);
        Destroy(SaveLoadManager.instance.gameObject);
    }
}