using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

[RequireComponent(typeof(AudioManager))]
public class BarkSystem : DogSystem
{
    void Update()
    {
        if (Input.GetKeyDown(selectedKey) && !isBark && !isDance)
        {
            isBark = true;
            DogAction();
        }

    }

}
