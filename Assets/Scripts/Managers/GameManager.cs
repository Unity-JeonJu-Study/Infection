using System.Collections;
using System.Collections.Generic;
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
    Intro,
    Pause,
    IntroScene,
    Laboratory,
    Stage1,
    Stage2,
    Stage3
}

public class GameManager : SerializedMonoBehaviour
{
    public static GameManager Instance;

    #region Stage Information
    
    [TabGroup("Stage Info")]
    public StageData stageData;
    [TabGroup("Stage Info")]
    public GameStage currentStage;
    [TabGroup("Stage Info")]
    public Queue<Quest> CurrentQuestList;
    [TabGroup("Stage Info")]
    [ReadOnly, SerializeField] private int currentStageTime;
    [ReadOnly, SerializeField] private Coroutine stageTimer;
    [ReadOnly, SerializeField] private bool isTimerPaused;

    #endregion

    // 현재 스테이지의 퀘스트를 하나씩 완료할 때 마다 CurrentQuestList에서 Deque를 호출
    [TabGroup("Quest Info")]
    public Quest currentGoal;
    
    public GameState currentState;
    
    // 맵 배치 상위 오브젝트
    
    public Transform roomParent;

    public Player player;
    private WaitForSeconds waitForOneSecond;

    private void Awake() {
        Instance = this;

        stageData = Resources.Load<StageData>("Data/Stage/StageData");
        currentStage = GameStage.Laboratory;
        currentState = GameState.StartUI;

        player = FindObjectOfType<Player>();
        InitStage();

        waitForOneSecond = new WaitForSeconds(1f);
    }

    private void InitStage()
    {
        foreach (var stage in stageData.data)
        {
            PoolManager.Instance.InitPool(stage.Value.gamePrefab, 1, roomParent);
        }
        UpdateStage(currentStage);
    }

    [Button("Update Stage Info & Prefab")]
    private void UpdateStage(GameStage stage)
    {
        // Update stage information
        currentStage = stage;
        CurrentQuestList = new Queue<Quest>(stageData.data[currentStage].quest.questList); // copy queue
        currentGoal = CurrentQuestList.Dequeue();
        
        // Activate stage
        for (int i = 0; i < roomParent.childCount; i++)
        {
            PoolManager.Instance.Despawn(roomParent.GetChild(i).gameObject);
        }
        PoolManager.Instance.Spawn(stageData.data[currentStage].gamePrefab.name);
    }

    [Button("Give me next Quest"),TabGroup("Quest Info")]
    public void NextQuest()
    {
        if (CurrentQuestList.Peek() == null)
        {
            Debug.Log("No More Quest Here");
            return;
        }
        currentGoal = CurrentQuestList.Dequeue();
    }

    #region Click Event

    public void OnClickPause() => currentState = GameState.PauseUI;
    public void OnClickStart() => currentState = GameState.StartUI;
    public void OnClickResume() => currentState = GameState.PlayingUI;

    #endregion

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Escape))
            PopupUIManager.instance.EnablePausePopup();
    }

    public void StartStage() {
        InGameUIManager.instance.UpdateObjectiveText(currentGoal.description);

        currentStageTime = ((int)GameManager.Instance.stageData.data[GameManager.Instance.currentStage].limitTime);
        isTimerPaused = false;
        stageTimer = StartCoroutine("StartStageTimer");

        InGameUIManager.instance.UpdateItemIcons();

        // add other codes required for stage start part
        // for instance, bgm can be started here
    }

    public IEnumerator StartStageTimer() {
        while(currentStageTime > 0) {
            if(isTimerPaused == false) {
                InGameUIManager.instance.UpdateTimeText(currentStageTime);
                currentStageTime--;
                yield return waitForOneSecond;
            }
        }

        StartGameOver();
    }

    public void PauseStageTimer() {
        isTimerPaused = true;
    }

    public void ResumeStageTimer() {
        isTimerPaused = false;
    }

    public void StopStageTimer() {
        if(stageTimer != null)
            StopCoroutine("StartStageTimer");
    }

    private void StartGameOver() {
        PopupUIManager.instance.EnableGameOverPopup();

        // add some codes required for game over part
    }
}