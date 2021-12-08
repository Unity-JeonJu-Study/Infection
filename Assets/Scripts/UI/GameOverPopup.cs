using UnityEngine;

// this script is temporary, after the core implementation is done, this script would be altered based on it
// things to change: restart method, main menu method

public class GameOverPopup : MonoBehaviour
{
    public void OnClickRestart() {
       // restart
       MySceneManager.instance.LoadScene("Choi Kang In");
    }

    public void OnClickMainMenu() {
        // scene manager part
       MySceneManager.instance.LoadScene("Temp Main Menu");
       
       Destroy(MySceneManager.instance.gameObject);
       Destroy(SaveLoadManager.instance.gameObject);
    }
}