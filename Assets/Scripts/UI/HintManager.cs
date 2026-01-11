using UnityEngine;
using UnityEngine.Events;

public class HintManager : MonoBehaviour
{
    public static HintManager Instance { get; private set; }
    GameObject hint;
    bool canInteract = false;
    public UnityEvent onInteractPressed = new UnityEvent();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void InitHint()
    {

        hint = hint != null ? hint : GameObject.FindGameObjectWithTag("hint");

        hint.SetActive(false);
    }

    private void Start()
    {
        InitHint();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canInteract)
        {
            onInteractPressed?.Invoke();
        }
    }

    public void ShowHint()
    {
        hint.SetActive(true);
        canInteract = true;
    }

    public void HideHint()
    {
        hint.SetActive(false);
        canInteract = false;
    }
}
