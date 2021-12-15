using UnityEngine;

public class GameOverPopup : MonoBehaviour
{
    public void OnClickRestart() {
        // restart
        MySceneManager.instance.loadedData = SaveLoadManager.instance.GetLatestData();
        if(MySceneManager.instance.loadedData.isLoaded)
            MySceneManager.instance.isInitial = false;
        else
            MySceneManager.instance.isInitial = true;

        MySceneManager.instance.LoadScene("Main Play Scene");
    }

    public void OnClickMainMenu() {
        // scene manager part
        MySceneManager.instance.LoadScene("Main Menu Scene");

        Destroy(MySceneManager.instance.gameObject);
    }
}