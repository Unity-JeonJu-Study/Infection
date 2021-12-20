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
    public SpawnPoint SpawnPoint;
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

    public GameObject mainCam;
    public Player player;
    private WaitForSeconds waitForOneSecond;
    private WaitForSeconds waitForTwoSeconds;

    private void Awake() {
        Instance = this;

        stageData = Resources.Load<StageData>("Data/Stage/StageData");
        currentStage = GameStage.Laboratory;
        currentState = GameState.StartUI;

        player = FindObjectOfType<Player>();
        mainCam = GameObject.FindWithTag("MainCamera");
        waitForOneSecond = new WaitForSeconds(1f);
        waitForTwoSeconds = new WaitForSeconds(2f);
    }

    private void Start()
    {
        if(MySceneManager.instance.isInitial)
            StartTutorialScene();       
        InitStage();    
    }
    
    [Button]
    public void StartTutorialScene()
    {
        InGameUIManager.instance.DisableAllInGameUIs();

        mainCam.SetActive(false);
        MySceneManager.instance.LoadCutScene("Tutorial");
    }
    public void SkipTutorialScene()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            MySceneManager.instance.UnLoadCutScene();
            EndTutorialScene();
        }
    }
    public void EndTutorialScene()
    {
        InGameUIManager.instance.EnableAllInGameUIs();
        mainCam.SetActive(true);
        UpdateStage(GameStage.Laboratory);
        
        if(!player.gameObject.activeSelf)
            player.gameObject.SetActive(true);
    }

    private void InitStage()
    {
        foreach (var stage in stageData.data)
        {
            PoolManager.Instance.InitPool(stage.Value.gamePrefab, 1, roomParent);
        }
    }

    [Button("Update Stage Info & Prefab")]
    public void UpdateStage(GameStage stage)
    {
        MySceneManager.instance.LoadScene("Main Play Scene", false);

        // Update stage information
        currentStage = stage;
        CurrentQuestList = new Queue<Quest>(stageData.data[currentStage].quest.questList); // copy queue
        currentGoal = CurrentQuestList.Peek();
        
        // Activate stage
        for (int i = 0; i < roomParent.childCount; i++)
        {
            PoolManager.Instance.Despawn(roomParent.GetChild(i).gameObject);
        }
        var newStage = PoolManager.Instance.Spawn(stageData.data[currentStage].gamePrefab.name);
        SpawnPoint = newStage.GetComponentInChildren<SpawnPoint>();
        player.transform.position = SpawnPoint.transform.position;
        
        // switch BGM when stage changed
        SoundManager.Instance.ClearBGM();
        SoundManager.Instance.PlayBGM(stageData.data[currentStage].bgm);

        InitInGameUIForCurrentStage();
    }

    [Button("Give me next Quest"),TabGroup("Quest Info")]
    public void NextQuest()
    {
        if (CurrentQuestList.Count == 0)
        {
            Debug.Log("No More Quest Here");
            return;
        }
        CurrentQuestList.Dequeue();
        currentGoal = CurrentQuestList.Peek();

        InGameUIManager.instance.UpdateObjectiveText(currentGoal.description, true);
    }

    public void ReSpawn()
    {
        UpdateStage(currentStage);
        InitInGameUIForCurrentStage();
    }
    
    #region Click Event

    public void OnClickPause() => currentState = GameState.PauseUI;
    public void OnClickStart() => currentState = GameState.StartUI;
    public void OnClickResume() => currentState = GameState.PlayingUI;

    #endregion

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Escape))
            PopupUIManager.instance.EnablePausePopup();
        SkipTutorialScene();
    }

    public void InitInGameUIForCurrentStage() {
        InGameUIManager.instance.UpdateObjectiveText("Rescue Slimes", false);
        InGameUIManager.instance.UpdateObjectiveText(currentGoal.description, true);

        InGameUIManager.instance.ResetSlimeSlots();

        if(GameManager.Instance.currentStage == GameStage.Laboratory)
            InGameUIManager.instance.DisableTimeText();
        else {
            InGameUIManager.instance.EnableTimeText();

            currentStageTime = ((int)stageData.data[GameManager.Instance.currentStage].limitTime);
            isTimerPaused = false;
            StopStageTimer();
            stageTimer = StartCoroutine("StartStageTimer");
        }

        // InGameUIManager.instance.UpdateItemIcons();
    }

    public IEnumerator StartStageTimer() {
        while(currentStageTime > 0) {
            if(isTimerPaused == false) {
                InGameUIManager.instance.UpdateTimeText(currentStageTime);
                currentStageTime--;
                yield return waitForOneSecond;
            }
            else
                break;
        }
        if(isTimerPaused == false)
            StartGameOver();
    }

    public void PauseStageTimer() {
        isTimerPaused = true;
    }

    public void ResumeStageTimer() {
        isTimerPaused = false;
        if(GameManager.Instance.currentStage != GameStage.Laboratory)
            stageTimer = StartCoroutine("StartStageTimer");
    }

    public void StopStageTimer() {
        if(stageTimer != null)
            StopCoroutine("StartStageTimer");
    }

    public void StartStageClear() {
        // if bgm exists, play it
        PopupUIManager.instance.EnableStageClearPopup(InGameUIManager.instance.GetRescuedSlimeCount(), stageData.data[currentStage].maxSlimeCount);
        StartCoroutine("WarpPlayerToLab"); 
    }

    private IEnumerator WarpPlayerToLab() {
        yield return waitForTwoSeconds;
        PopupUIManager.instance.DisableStageClearPopup();
    }

    private void StartGameOver() {
        StopStageTimer();
        PopupUIManager.instance.EnableGameOverPopup();

        // add some codes required for game over part
    }
}