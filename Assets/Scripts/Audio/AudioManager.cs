using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] AudioMixerGroup audioMixer;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = audioMixer;
    }

    public void PlaySound(AudioClip clip, float volume = 1.0f, float pitch = 1.0f)
    {
        if (clip == null) return;

        audioSource.volume = volume;
        audioSource.pitch = pitch;
        audioSource.PlayOneShot(clip);
    }

    public void StopSound()
    {
        audioSource.Stop();
    }

}
