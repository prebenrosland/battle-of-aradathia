using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_walk : StateMachineBehaviour
{
    public float speed = 2.5f;
    public float minDistance = 5f;
    public float stopDistance = 1f;
    public float rotationSpeed = 5f;

    private Transform player;
    private Rigidbody rb;
    private Animator animator;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.gameObject.GetComponent<Rigidbody>();
        this.animator = animator;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float distance = Vector3.Distance(rb.position, player.position);

        // Rotate the boss towards the player
        Vector3 lookDirection = (player.position - animator.transform.position).normalized;
        lookDirection.y = 0;
        Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
        animator.transform.rotation = Quaternion.Slerp(animator.transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

        if (distance < minDistance && distance > stopDistance)
        {
            Vector3 direction = (player.position - animator.transform.position).normalized;
            direction.y = 0;
            animator.transform.Translate(direction * speed * Time.deltaTime, Space.World);

            animator.SetBool("IsInRange", true);
        }
        else
        {
            animator.SetBool("IsInRange", false);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
