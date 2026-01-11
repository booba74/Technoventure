using UnityEngine;
using UnityEngine.Audio;

public class AudioRandomOn : MonoBehaviour
{
    public AudioClip[] audioClips;
    private AudioSource audioSource;
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    // Update is called once per frame
    public void RandomPlaying()
    {
        audioSource.clip = audioClips[Random.Range(0, audioClips.Length)];
        audioSource.Play();
    }
}
