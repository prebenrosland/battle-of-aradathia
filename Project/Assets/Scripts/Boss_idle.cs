using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_idle : StateMachineBehaviour
{
    public float minDistance = 5f;
    public float stopDistance = 1f;

    private Transform player;
    private Animator animator;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        this.animator = animator;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float distance = Vector3.Distance(animator.transform.position, player.position);

        if (distance < minDistance && distance > stopDistance)
        {
            animator.SetBool("IsInRange", true);
        }
        else
        {
            animator.SetBool("IsInRange", false);
        }
    }
}
