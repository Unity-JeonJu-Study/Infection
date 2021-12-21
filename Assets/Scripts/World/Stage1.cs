using System;
using System.Collections;
using UnityEngine;

public class Stage1 : MonoBehaviour
{
    public GameObject finalDoor;
    public bool flag;



    private void Start()
    {
        InitSetting();
    }

    private void OnEnable()
    {
        InitSetting();
    }

    public void InitSetting()       
    {
        if (finalDoor != null)
            finalDoor.SetActive(false);
        SoundManager.Instance.PlayBGM("jungle");
    }

    private void Update()
    {
        StartCoroutine(Stage1Clear());
    }

    private IEnumerator Stage1Clear()
    {
        if (flag)
            yield break;
        var x = InGameUIManager.instance.GetRescuedSlimeCount();
        if (GameManager.Instance.stageData.data[GameStage.Stage1].minSlimeCount <= x)
        {
            finalDoor.SetActive(true);
            GameManager.Instance.StartStageClear();
            flag = true;
            yield return new WaitForSeconds(2f);
            GameManager.Instance.NextQuest();
        }
    }


}