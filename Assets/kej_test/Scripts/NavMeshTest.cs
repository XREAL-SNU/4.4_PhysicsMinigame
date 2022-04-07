using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshTest : MonoBehaviour
{
    NavMeshAgent agent;

    [SerializeField] GameObject target_1;
    [SerializeField] GameObject target_2;
    [SerializeField] GameObject target_3;
    [SerializeField] GameObject target_4;

    Transform target;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        target = target_1.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        // agent.SetDestination(target.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Equals(target_1.name))
        {
            target = target_2.GetComponent<Transform>();
        }
        else if (other.gameObject.name.Equals(target_2.name))
        {
            target = target_3.GetComponent<Transform>();
        }
        else if (other.gameObject.name.Equals(target_3.name))
        {
            target = target_4.GetComponent<Transform>();
        }
        else { }
    }

    public void SetActiveMove()
    {
        agent.SetDestination(target.position);
    }
}
