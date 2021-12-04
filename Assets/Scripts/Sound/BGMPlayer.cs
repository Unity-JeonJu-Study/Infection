
using DG.Tweening;
using UnityEngine;

public class BGMPlayer : MonoBehaviour
{
     public AudioSource _audioSource;
     private Transform _transform;
     private float originalVolume;
     private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _transform = GetComponent<Transform>();
    }

    public void Play(AudioClip clip, float volume)
    {
        _audioSource.loop = true;
        _audioSource.clip = clip;
        _audioSource.volume = volume * SoundManager.Instance.masterVolumeBGM;
        originalVolume = volume;
        _audioSource.Play();
    }

    public void Mute()
    {
        _audioSource.volume = 0f;
    }

    public void LerpMute()
    {
        _audioSource.DOFade(0f, SoundManager.Instance.bgmLerpDuration);
    }
    
    public void UnMute()
    {
        _audioSource.volume = originalVolume * SoundManager.Instance.masterVolumeBGM;
    }

    public void LerpUnMute()
    {
        _audioSource.DOFade(originalVolume * SoundManager.Instance.masterVolumeBGM, SoundManager.Instance.bgmLerpDuration);
    }

    public void SetVolume()
    {
        _audioSource.DOFade(originalVolume * SoundManager.Instance.masterVolumeBGM, SoundManager.Instance.bgmLerpDuration);
    }
}
