using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioManager))]
public class DogSystem : MonoBehaviour
{
    [SerializeField] protected AudioClip sound;
    protected AudioManager audioManager;

    protected DogZone dogZone;

    [SerializeField] protected float cooldown = 2;
    [SerializeField] protected float radius = 5;

    protected bool isDance = false;
    protected bool isBark = false;

    [SerializeField] protected KeyCode selectedKey;


    private void Start()
    {
        audioManager = GetComponent<AudioManager>();
        dogZone = GetComponent<DogZone>();
    }

    protected void DogAction()
    {

        StartCoroutine("IDogAction");

    }

    IEnumerator IDogAction()
    {
        audioManager.PlaySound(sound);
        if (isBark) dogZone.TriggerFear();
        if (isDance) dogZone.TriggerDance();
        yield return new WaitForSeconds(cooldown);
        if (isBark) isBark = false;
        if (isDance) isDance = false;

    }
}
