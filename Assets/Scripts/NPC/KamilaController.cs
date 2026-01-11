using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class KamilaController : MonoBehaviour
{
    [SerializeField] string[] questNames;
    TaskManager taskManager;
    int index = 0;
    private void Start()
    {
        taskManager = FindFirstObjectByType<TaskManager>();
    }

    private void Update()
    {

    }

    public void UpdateProgress(string questName)
    {
        for (int i = 0; i < questNames.Length; i++)
        {
            if (questNames[i] == questName)
            {
                index = i;
                break;
            }
        }

        if (PlayerPrefs.GetInt(questNames[index]) == 1)
        {
            taskManager.UpdateTextTask(index + 1);
        }
        if (PlayerPrefs.GetInt(questNames[index]) == 3)
        {
            taskManager.UpdateTextTask(0);
        }
    }
}
