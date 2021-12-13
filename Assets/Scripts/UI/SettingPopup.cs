using UnityEngine;
using UnityEngine.UI;

public class SettingPopup : MonoBehaviour
{
    [ReadOnly, SerializeField] private Slider sliderSFXSound;
    [ReadOnly, SerializeField] private Slider sliderBGMSound;

    private void Awake() {
        Slider[] sliders = GetComponentsInChildren<Slider>();
        sliderSFXSound = sliders[0];
        sliderBGMSound = sliders[1];
    }

    private void Start() {
        sliderSFXSound.value = SoundManager.Instance.masterVolumeSFX * 100;
        sliderBGMSound.value = SoundManager.Instance.masterVolumeBGM * 100;
    }

    public void OnClickClose() {
        gameObject.SetActive(false);
    }

    public void OnSFXSoundValueChanged() {
        SoundManager.Instance.SetVolumeSFX(sliderSFXSound.value/100f);
    }

    public void OnBGMSoundValueChanged() {
        SoundManager.Instance.SetVolumeBGM(sliderBGMSound.value/100f);
    }
}