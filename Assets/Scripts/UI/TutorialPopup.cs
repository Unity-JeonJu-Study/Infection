using UnityEngine;

public class TutorialPopup : MonoBehaviour
{
    private void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            GameManager.Instance.ResumeStageTimer();
            gameObject.SetActive(false);
        }
    }
}