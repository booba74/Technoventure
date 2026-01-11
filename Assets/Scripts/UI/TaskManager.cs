using UnityEngine;
using UnityEngine.UI;

public class TaskManager : MonoBehaviour
{
    [SerializeField] string[] textTask;
    Text task;
    void Start()
    {
        task = GameObject.FindGameObjectWithTag("taskText").GetComponent<Text>();
        UpdateTextTask(0);
    }
    
    public void UpdateTextTask(int step)
    {
        task.text = textTask[step];
    }
}
