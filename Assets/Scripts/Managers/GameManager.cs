using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

public enum GameState
{
    StartUI,
    PauseUI,
    PlayingUI
}
public enum GameStage
{
    Laboratory,
    Zoo,
    Stage1,
    Stage2,
    Stage3
}

public class GameManager : SerializedMonoBehaviour
{
    public static GameManager Instance;
    
    public List<GameObject> stagePrefabs = new List<GameObject>();
    [Tooltip("Game Stage")]
    public GameStage currentStage;
    [Tooltip("Game State")]
    public GameState currentState;

    // 맵 배치 상위 오브젝트
    public Transform roomParent;

    private void Awake() {
        Instance = this;
        
        currentStage = GameStage.Laboratory;
        currentState = GameState.StartUI;

        InitRoom();
    }

    private void InitRoom()
    {
        var stageResource = Resources.Load<StageData>("Data/Stage/StageData").data;
        
        foreach (var stage in stageResource)
        {
            stagePrefabs.Add(Instantiate(stage.Value, roomParent));
        }
        MoveStage();
    }

    [Button("Execute")]
    public void SetStage(GameStage stage)
    {
        currentStage = stage;
        MoveStage();
    }
    private void MoveStage()
    {
        foreach (var stage in stagePrefabs)
        {
            stage.SetActive(false);
        }
        stagePrefabs[(int)currentStage].SetActive(true);
    }
    
    #region Click Event

    public void OnClickPause() => currentState = GameState.PauseUI;
    public void OnClickStart() => currentState = GameState.StartUI;
    public void OnClickResume() => currentState = GameState.PlayingUI;

    #endregion
}

