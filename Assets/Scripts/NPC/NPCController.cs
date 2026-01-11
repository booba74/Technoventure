using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using System.Collections.Generic;

[RequireComponent(typeof(NavMeshAgent))]
public class NPCController : MonoBehaviour
{
    NavMeshAgent agent;

    [SerializeField] float fearDistance = 10f;
    [SerializeField] float fearTime = 1f;

    [SerializeField] float idleTime = 5f;

    [SerializeField] float agentSpeed = 3.5f;

    Animator anim;

    public List<Transform> points;
    void Start()
    {
       
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        SetDestination();
    }
    void Update()
    {
        if (agent.remainingDistance < .25f)
        {
            StartCoroutine("Idle");
        }
    }
    public void SetDestination()
    {
        Vector3 newTarget = points[Random.Range(0, points.Count)].position;
        agent.SetDestination(newTarget);
    }
 
    IEnumerator Idle()
    {
        agent.speed = 0;
        SetDestination();
        anim.SetBool("idle", true);
        yield return new WaitForSeconds(idleTime);
        agent.speed = agentSpeed;
        anim.SetBool("idle", false);

    }


    public void Fear(Vector3 dogPos)
    {
        StartCoroutine(IFear(dogPos));
    }
    IEnumerator IFear(Vector3 dogPos)
    {
        agent.speed = agentSpeed;
        Vector3 dirAway = (transform.position - dogPos).normalized;
        Vector3 runTarget = transform.position + dirAway * fearDistance;

        if (NavMesh.SamplePosition(runTarget, out NavMeshHit hit, fearDistance, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
        yield return new WaitForSeconds(fearTime);
        agent.ResetPath();
    }
}
