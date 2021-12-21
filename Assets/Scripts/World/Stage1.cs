using UnityEngine;

public class Stage1 : MonoBehaviour
{
    public GameObject finalDoor;
    public bool flag;
    private void Start()
    {
        if(finalDoor != null)
            finalDoor.SetActive(false);
    }

    private void Update()
    {
        if (flag)
            return;
        var x = InGameUIManager.instance.GetRescuedSlimeCount();
        if (GameManager.Instance.stageData.data[GameStage.Stage1].minSlimeCount <= x)
        {
            finalDoor.SetActive(true);
            GameManager.Instance.StartStageClear();
            GameManager.Instance.NextQuest();
            flag = true;
        }
    }
}