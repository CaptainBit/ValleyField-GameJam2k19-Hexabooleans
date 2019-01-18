using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Agent : MonoBehaviour
{

    private GameManager manager;
    private Vector3 initialPosition;

    public Transform target;

    NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        initialPosition = agent.transform.position;
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        StartCoroutine(run());
    }

    void RunToPlayer()
    {
       agent.SetDestination(target.position);
      
    }

    IEnumerator run()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);
            RunToPlayer();
            yield return new WaitForSeconds(15f);
            agent.SetDestination(initialPosition);
        }
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnTriggerEnter(Collider other)
    {
        manager.AgentCollidePlayer();
        agent.transform.position = initialPosition;
    }
}
