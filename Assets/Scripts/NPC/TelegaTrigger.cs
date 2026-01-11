using UnityEngine;

public class TelegaTrigger : MonoBehaviour
{
    DialogueManager dialogue;
    bool isGet = false;
    [SerializeField] GameObject firstKamila;
    [SerializeField] GameObject secondKamila;
    [SerializeField] Transform point;

    [SerializeField] float increaseMood;

    TaskManager kam;

    //HappyBarController happyBar;
    private void Start()
    {
        dialogue = firstKamila.GetComponent<DialogueManager>();
        //happyBar = FindFirstObjectByType<HappyBarController>();
        kam = FindFirstObjectByType <TaskManager>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "telega")
        {
            if (!isGet)
            {
                dialogue.QuestSet("telegaQuest");
                Destroy(firstKamila);
                secondKamila.transform.position = point.position;
                //happyBar.UpdateBar(increaseMood);
                //PlayerPrefs.SetInt("getWater", 3);
                //PlayerPrefs.SetInt("telegaQuest", 3);
                //kam.UpdateTextTask(0);
                isGet = true;
            }
        }
    }

    private void Update()
    {
       
    }
}
