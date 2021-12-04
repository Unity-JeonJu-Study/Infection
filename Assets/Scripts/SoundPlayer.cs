using System.Collections;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
     public AudioSource _audioSource;
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void SoundPlayOneShot(AudioClip clip, float volume)
    {
        StartCoroutine(DespawnSound(clip.length));
        _audioSource.mute = false;
        _audioSource.PlayOneShot(clip, volume);
    }
    
    public void SoundPlayLoop(AudioClip clip, float volume)
    {
        _audioSource.clip = clip;
        _audioSource.volume = volume;
        _audioSource.loop = true;
        _audioSource.Play();
    }

    IEnumerator DespawnSound(float time)
    {
        yield return new WaitForSeconds(time);
        _audioSource.mute = true;
        PoolManager.Instance.Despawn(gameObject);
    }
}
