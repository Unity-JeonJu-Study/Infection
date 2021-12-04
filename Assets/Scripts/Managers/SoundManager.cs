using System;
using UnityEngine;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Random = UnityEngine.Random;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public int maxMainBGMLayerCount = 8;
    public float bgmLerpDuration = 0.5f;
    [ReadOnly] private int currentMainBGMLayer;

    public float masterVolumeSFX = 1f;
    public float masterVolumeBGM = 1f;

    [ReadOnly]
    public AudioClip[] BGMClip; // 오디오 소스들 지정.
    [ReadOnly]
    public AudioClip[] audioClip; // 오디오 소스들 지정.

    [ReadOnly]
    public Dictionary<string, AudioClip> audioClipsDic = new Dictionary<string, AudioClip>();
    [ReadOnly]
    public Dictionary<string, AudioClip> BGMClipsDic = new Dictionary<string, AudioClip>();

    [ReadOnly]
    public Dictionary<string, BGMPlayer> BGMPlayers = new Dictionary<string, BGMPlayer>();

    void Awake()
    {
        Instance = this;
        audioClip = Resources.LoadAll<AudioClip>("Sounds/SFX");
        BGMClip =Resources.LoadAll<AudioClip>("Sounds/BGM");

        audioClipsDic = new Dictionary<string, AudioClip>();
        foreach (AudioClip a in audioClip)
        {
            audioClipsDic.Add(a.name, a);
        }

        BGMClipsDic = new Dictionary<string, AudioClip>();
        foreach (AudioClip a in BGMClip)
        {
            //Debug.Log("BGM AddKey: " + a.name);
            BGMClipsDic.Add(a.name, a);
        }
        
        BGMPlayers = new Dictionary<string, BGMPlayer>();
    }

    private void Start()
    {
        PoolManager.Instance.InitPool(Resources.Load<GameObject>("SoundPlayer"), 10); 
        PoolManager.Instance.InitPool(Resources.Load<GameObject>("BGMPlayer"), 3); 
    }

    public void PlayBGM(string a_name, float a_volume = 1f)
    {
        if (BGMClipsDic.ContainsKey(a_name) == false)
        {
            Debug.Log(a_name + " is not Contained audioClipsDic");
            return;
        }

        // var bgmPlayer = PoolManager.Pools["BGMs"]
        //     .Spawn("BGMPlayer")
        //     .GetComponent<BGMPlayer>();
        var bgmPlayer = PoolManager.Instance.Spawn("BGMPlayer").GetComponent<BGMPlayer>();
        BGMPlayers.Add(a_name, bgmPlayer);
        bgmPlayer.Play(BGMClipsDic[a_name], a_volume);
    }

    // 한 번 재생 : 볼륨 매개변수로 지정
    public SoundPlayer PlaySound(string a_name, float a_volume = 1f)
    {
        if (audioClipsDic.ContainsKey(a_name) == false)
        {
            Debug.Log(a_name + " is not Contained audioClipsDic");
            return null;
        }
        var soundPlayer = PoolManager.Instance.Spawn("SoundPlayer").GetComponent<SoundPlayer>();
        soundPlayer.SoundPlayOneShot(audioClipsDic[a_name], a_volume * masterVolumeSFX);
        return soundPlayer;
    }

    // 랜덤으로 한 번 재생 : 볼륨 매개변수로 지정
    public void PlayRandomSound(string[] a_nameArray, float a_volume = 1f)
    {
        string l_playClipName;

        l_playClipName = a_nameArray[Random.Range(0, a_nameArray.Length)];

        if (audioClipsDic.ContainsKey(l_playClipName) == false)
        {
            Debug.Log(l_playClipName + " is not Contained audioClipsDic");
            return;
        }

        var soundPlayer = PoolManager.Instance.Spawn("SoundPlayer").GetComponent<SoundPlayer>();
        soundPlayer.SoundPlayOneShot(audioClipsDic[l_playClipName], a_volume * masterVolumeSFX);
    }

    // 삭제할때는 리턴값은 GameObject를 참조해서 삭제한다. 나중에 옵션에서 사운드 크기 조정하면 이건 같이 참조해서 바뀌어야함..
    public GameObject PlayLoopSound(string a_name, float a_volume = 1f)
    {
        if (audioClipsDic.ContainsKey(a_name) == false)
        {
            Debug.Log(a_name + " is not Contained audioClipsDic");
            return null;
        }
        
        var soundPlayer = PoolManager.Instance.Spawn("SoundPlayer").GetComponent<SoundPlayer>();
        soundPlayer.SoundPlayLoop(audioClipsDic[a_name], a_volume * masterVolumeSFX);

        return soundPlayer.gameObject;
    }

    // 주로 전투 종료시 음악을 끈다.
    public void ClearBGM()
    {
        foreach (var bgmPair in BGMPlayers)
        {
           // PoolManager.Pools["BGMs"].Despawn(bgmPair.Value.transform);
           PoolManager.Instance.Despawn(bgmPair.Value.gameObject);
        }
        BGMPlayers.Clear();
    }
    
    /// <summary>
    /// -
    /// </summary>
    /// <param name="firstStartLayerId">-1: AllMute, 3: Map</param>
    public void StartMainBGM(int firstStartLayerId)
    {
        ClearBGM();
        currentMainBGMLayer = firstStartLayerId;
        for (var i = 1; i <= maxMainBGMLayerCount; i++)
        {
            PlayBGM($"Industrial Combat LAYER {i}");
            
            if (i == firstStartLayerId)
                BGMPlayers[$"Industrial Combat LAYER {i}"].UnMute();
            else
                BGMPlayers[$"Industrial Combat LAYER {i}"].Mute();
        }
    }

    public void ChangeMainBGMLayer(int layerId)
    {
        currentMainBGMLayer = layerId;
        for (var i = 1; i <= maxMainBGMLayerCount; i++)
        {
            if (!BGMPlayers.ContainsKey($"Industrial Combat LAYER {i}"))
                continue;
            if (i == layerId)
                BGMPlayers[$"Industrial Combat LAYER {i}"].LerpUnMute();
            else
            {
                BGMPlayers[$"Industrial Combat LAYER {i}"].LerpMute();    
            }
            
        }
    }

    #region 옵션에서 볼륨조절
    public void SetVolumeSFX(float a_volume)
    {
        masterVolumeSFX = a_volume;
    }

    public void SetVolumeBGM(float a_volume)
    {
        masterVolumeBGM = a_volume;
        foreach (var players in BGMPlayers)
        {
            if (players.Value._audioSource.clip.name.Contains("Industrial Combat LAYER")
                && players.Value._audioSource.clip.name != $"Industrial Combat LAYER {currentMainBGMLayer}")
            {
                continue;
            }
            players.Value.SetVolume();
        }
    }
    #endregion
}

[Serializable]
public class SoundData
{
    public AudioClip soundClip;
    public float soundVolume = 1f;
}
