using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScene : MonoBehaviour
{
    public void UnLoadScene()
    {
        MySceneManager.instance.UnLoadCutScene();
        GameManager.Instance.EndTutorialScene();
    }
}
