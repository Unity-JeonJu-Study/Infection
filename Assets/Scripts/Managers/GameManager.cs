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

public enum BGMList
{
    
}

public class GameManager : SerializedMonoBehaviour
{
    public static GameManager Instance;
    
    //public Dictionary<GameStage, GameObject> stagePrefabs = new Dictionary<GameStage, GameObject>();
    public StageData stageData;
    [Tooltip("Game Stage")]
    public GameStage currentStage;
    [Tooltip("Game State")]
    public GameState currentState;

    // 맵 배치 상위 오브젝트
    public Transform roomParent;

    public Player player;

    private void Awake() {
        Instance = this;

        stageData = Resources.Load<StageData>("Data/Stage/StageData");
        currentStage = GameStage.Laboratory;
        currentState = GameState.StartUI;
        
        player = FindObjectOfType<Player>();
        InitRoom();
    }

    private void InitRoom()
    {
        foreach (var stage in stageData.data)
        {
            PoolManager.Instance.InitPool(stage.Value.gamePrefab, 1, roomParent);
        }
        MoveStage();
    }

    [Button("방 변경 트리거")]
    public void SetStage(GameStage stage)
    {
        currentStage = stage;
        MoveStage();
    }
    private void MoveStage()
    {
        for (int i = 0; i < roomParent.childCount; i++)
        {
            PoolManager.Instance.Despawn(roomParent.GetChild(i).gameObject);
        }
        PoolManager.Instance.Spawn(stageData.data[currentStage].gamePrefab.name);
    }
    
    #region Click Event

    public void OnClickPause() => currentState = GameState.PauseUI;
    public void OnClickStart() => currentState = GameState.StartUI;
    public void OnClickResume() => currentState = GameState.PlayingUI;

    #endregion
}

