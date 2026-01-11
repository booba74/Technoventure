using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Speaker
{
    Player,
    NPC
}

[RequireComponent(typeof(Collider))]
public class DialogueManager : MonoBehaviour
{
    public List<DialogueLine> dialogueLines;
    public float typingSpeed = 0.05f;

    public GameObject dialoguePanel;
    public GameObject playerPanel;
    public GameObject npcPanel;
    public Text playerText;
    public Text npcText;
    public Image playerImage;
    public Image npcImage;

    public bool showHint = true;

    int currentLineIndex = 0;
    bool isTyping = false;
    bool inDialogue = false;
    bool playerInTrigger = false;

    Coroutine typingCoroutine;

    public string questName;
    public int needQuestStep;
    public int questStep;
    public enum QuestStep
    {
        NoQuest,
        GetQuest,
        DoneTask,
        EndQuest
    }

    KamilaController kamila;

    private void Start()
    {
        kamila = FindFirstObjectByType<KamilaController>();
        PlayerPrefs.DeleteAll();
        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(false);
            playerPanel.SetActive(false);
            npcPanel.SetActive(false);
        }
        HintManager.Instance.onInteractPressed.AddListener(StartDialogue);

        if (PlayerPrefs.HasKey(questName))
        {
            questStep = PlayerPrefs.GetInt(questName);
            if (questStep != needQuestStep)
            {
                EndDialogue();
            }
        }
    }

    private void Update()
    {
        print(questStep);

        if (inDialogue && Input.GetMouseButtonDown(0))
        {
            ContinueDialogue();
        }

    }

    private void OnTriggerEnter(Collider other)
    {


        if (other.CompareTag("Human"))
        {
            if (questStep != needQuestStep)
            {
                EndDialogue();
                return;
            }
            playerInTrigger = true;
            if (showHint && !inDialogue)
            {
                HintManager.Instance.ShowHint();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Human"))
        {
            playerInTrigger = false;
            if (showHint)
            {
                HintManager.Instance.HideHint();
            }
            if (inDialogue)
            {
                EndDialogue();
            }
        }
    }

    public void QuestSet(string questName)
    {
        questStep = 2;
        PlayerPrefs.SetInt(questName, 2);
    }

    public void StartDialogue()
    {
        if (playerInTrigger && !inDialogue)
        {
            if (dialogueLines.Count == 0 || inDialogue) return;

            inDialogue = true;
            currentLineIndex = 0;

            if (dialoguePanel != null)
            {
                dialoguePanel.SetActive(true);
            }

            if (showHint)
            {
                HintManager.Instance.HideHint();
            }

            DisplayCurrentLine();
        }
    }

    private void DisplayCurrentLine()
    {
        if (currentLineIndex >= dialogueLines.Count) return;

        var line = dialogueLines[currentLineIndex];
        bool isPlayer = line.speaker == Speaker.Player;

        if (playerPanel != null) playerPanel.SetActive(isPlayer);
        if (npcPanel != null) npcPanel.SetActive(!isPlayer);

        typingCoroutine = StartCoroutine(TypeText(line));
    }

    private IEnumerator TypeText(DialogueLine line)
    {
        isTyping = true;
        Text targetText = line.speaker == Speaker.Player ? playerText : npcText;

        if (targetText != null)
        {
            targetText.text = "";
            foreach (char letter in line.text.ToCharArray())
            {
                targetText.text += letter;
                yield return new WaitForSeconds(typingSpeed);
            }
        }

        isTyping = false;
    }

    public void ContinueDialogue()
    {
        if (isTyping)
        {
            StopCoroutine(typingCoroutine);
            isTyping = false;
            DisplayFullCurrentLine();
            return;
        }

        currentLineIndex++;
        if (currentLineIndex < dialogueLines.Count)
        {
            DisplayCurrentLine();
        }
        else
        {
            EndDialogue();
            if (questStep == needQuestStep)
            {
                questStep =
           questStep == (int)QuestStep.NoQuest ? (int)QuestStep.GetQuest : (int)QuestStep.EndQuest;
                PlayerPrefs.SetInt(questName, questStep);
                kamila.UpdateProgress(questName);
            }
        }
    }

    private void DisplayFullCurrentLine()
    {
        if (currentLineIndex >= dialogueLines.Count) return;

        var line = dialogueLines[currentLineIndex];
        Text targetText = line.speaker == Speaker.Player ? playerText : npcText;

        if (targetText != null)
        {
            targetText.text = line.text;
        }
    }

    public void EndDialogue()
    {

        inDialogue = false;

        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(false);
            playerPanel.SetActive(false);
            npcPanel.SetActive(false);
        }

        if (playerInTrigger && showHint)
        {
            HintManager.Instance.ShowHint();
        }
    }

    private void OnDestroy()
    {
        if (inDialogue)
        {
            EndDialogue();
        }
    }
}
[System.Serializable]
public class DialogueLine
{
    public Speaker speaker;
    [TextArea(3, 10)]
    public string text;
}






