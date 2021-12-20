using System;
using UnityEngine;

public class Stage1 : MonoBehaviour
{
    public GameObject finalDoor;
    private void Start()
    {
        if(finalDoor != null)
            finalDoor.SetActive(false);
    }

    private void Update()
    {
        var x = InGameUIManager.instance.GetRescuedSlimeCount();

        if (GameManager.Instance.stageData.data[GameStage.Stage1].minSlimeCount <= x)
        {
            finalDoor.SetActive(true);
            GameManager.Instance.StartStageClear();
        }
    }
}
