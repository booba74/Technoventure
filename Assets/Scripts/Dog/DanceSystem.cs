using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class DanceSystem : DogSystem
{
    void Update()
    {
        if (Input.GetKeyDown(selectedKey) && !isBark && !isDance)
        {
            isDance = true;
            DogAction();

        }

    }
}
