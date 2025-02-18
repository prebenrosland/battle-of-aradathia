using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCNavMesh : MonoBehaviour
{
    [SerializeField] private Transform[] movePositionTransform;
    private int destinationPoint = 0;
    private NavMeshAgent navMeshAgent;
    private Animator anim;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        navMeshAgent.autoBraking = false;
        StartCoroutine (GoToNextPont() );
    }

    IEnumerator GoToNextPont() {
        if (movePositionTransform.Length == 0)
            yield break;

        navMeshAgent.destination = movePositionTransform[destinationPoint].position;
        destinationPoint = (destinationPoint + 1) % movePositionTransform.Length;
    }   

    private void Update() {
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.5f) {
            StartCoroutine (GoToNextPont() );
        }
    }
}
