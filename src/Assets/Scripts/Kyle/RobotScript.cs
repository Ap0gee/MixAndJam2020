using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RobotScript : MonoBehaviour
{
    public Transform MoveTarget;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void Load(Vector3 point)
    {
        NavMeshAgent agent = this.GetComponent<NavMeshAgent>();
        agent.Warp(point);
    }

    // Update is called once per frame
    void Update()
    {
        NavMeshAgent agent = this.GetComponent<NavMeshAgent>();
        agent.destination = this.MoveTarget.position;
    }
}
