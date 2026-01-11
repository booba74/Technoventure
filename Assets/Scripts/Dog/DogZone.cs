using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class DogZone : MonoBehaviour
{
    [SerializeField] float _fearRadius = 5f;

    [SerializeField] float decreaseMood = 5f;
    [SerializeField] float increaseMood = 5f;
   // HappyBarController happyBar;

    private void Awake()
    {
        //happyBar = FindFirstObjectByType<HappyBarController>();
        GetComponent<SphereCollider >().isTrigger = true;
        var sphereCollider = GetComponent<SphereCollider>();
        sphereCollider.radius = _fearRadius;
    }

    public void TriggerFear()
    {
        Collider[] npcs = Physics.OverlapSphere(transform.position, _fearRadius);
        foreach (var npc in npcs)
        {
            if (npc.TryGetComponent(out NPCController fearReact))
            {
                print("aaaaaaaaaaaaaaaa");
                fearReact.Fear(transform.position);
                //happyBar.UpdateBar(-decreaseMood);
            }
        }
    }
    public void TriggerDance()
    {
        Collider[] npcs = Physics.OverlapSphere(transform.position, _fearRadius);
        foreach (var npc in npcs)
        {
            if (npc.TryGetComponent(out NPCController fearReact))
            {
               // happyBar.UpdateBar(increaseMood);
            }
        }
    }
}