using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.Serialization;
using UnityEngine;

public enum GameState
{
    StartUI,
    PauseUI,
    PlayingUI
}


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [OdinSerialize, Tooltip("Game Stage")]
    public int stageIdx;
    [OdinSerialize, Tooltip("Game State")]
    public GameState state;

    private void Awake() {
        Instance = this;
        stageIdx = 0;
        state = GameState.StartUI;
    }

    #region Click Event

    public void OnClickPause() => state = GameState.PauseUI;
    public void OnClickStart() => state = GameState.StartUI;
    public void OnClickResume() => state = GameState.PlayingUI;

    #endregion



}

