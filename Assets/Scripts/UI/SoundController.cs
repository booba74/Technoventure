using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{
    [SerializeField] AudioMixer masterMixer;
    [SerializeField] Slider musicVolumeSlider, soundVolumeSlider;
    float musicLvl, soundLvl;

    private void Start()
    {
        CheckSaves("musicVolume", musicVolumeSlider);
        CheckSaves("soundVolume", soundVolumeSlider);
    }

    void CheckSaves(string key, Slider slider)
    {
        if (PlayerPrefs.HasKey(key))
        {
            slider.value = PlayerPrefs.GetFloat(key);
            masterMixer.SetFloat(key, PlayerPrefs.GetFloat(key));
        }
        else
        {
            slider.value = 0.0f;
            masterMixer.SetFloat(key, 0.0f);
        }
    }
    public void SetMusicVolume()
    {
        musicLvl = musicVolumeSlider.value;
        masterMixer.SetFloat("musicVolume", musicLvl);
        PlayerPrefs.SetFloat("musicVolume", musicLvl);
    }
    public void SetSoundVolume()
    {
        soundLvl = soundVolumeSlider.value;
        masterMixer.SetFloat("soundVolume", soundLvl);
        PlayerPrefs.SetFloat("soundVolume", soundLvl);
    }
}
