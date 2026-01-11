using UnityEngine;
using static DialogueManager;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class BabkaTouch : MonoBehaviour
{
    DialogueManager dialogue;
    bool isGet = false;
    [SerializeField] GameObject firstKamila;
    [SerializeField] GameObject secondKamila;
    [SerializeField] Transform point;

    [SerializeField] GameObject triggerZone;

    [SerializeField] float increaseMood;

    //HappyBarController happyBar;
    private void Start()
    {
        dialogue = GetComponent<DialogueManager>();
        //happyBar = FindFirstObjectByType<HappyBarController>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "bottle")
        {
            dialogue.QuestSet("getWater");
            Destroy(other.gameObject);
        }
    }

    private void Update()
    {
        if (!isGet)
        {
            if (PlayerPrefs.GetInt("getWater") == 3)
            {
                Destroy(firstKamila);
                secondKamila.transform.position = point.position;
                //happyBar.UpdateBar(increaseMood);
                triggerZone.SetActive(true);
                isGet = true;
            }
        }
    }
}
