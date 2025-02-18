using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;
using System;

public class SkeletonAI : MonoBehaviour
{
    public GameObject skeletonGameObject;
    public float attackCooldown = 2f;
    public float attackDistance = 2f;
    public Transform playerTransform;
    public float returnSpeed = 1f;

    public Transform[] waypoints;
    private int currentWaypointIndex = 0;

    private Animator skeletonAnimator;
    private SkeletonStats skeletonStats;
    private float timeSinceLastAttack = 0f;
    private int numberOfAttacks = 3;
    private int previousAttackNumber = 0;
    private NavMeshAgent navMeshAgent;

    public float despawnDelay = 60f;
    private float timeSinceDeath = 0f;
    private bool startDespawnTimer = false;
    private Collider skeletonCollider;

    public float sightRange;
    public bool playerInSightRange;

    private Vector3 originalPosition;

    void Start()
    {
        skeletonAnimator = skeletonGameObject.GetComponent<Animator>();
        skeletonStats = skeletonGameObject.GetComponent<SkeletonStats>();
        navMeshAgent = skeletonGameObject.GetComponent<NavMeshAgent>();
        skeletonCollider = skeletonGameObject.GetComponent<Collider>();

        originalPosition = transform.position;

        Player player = FindObjectOfType<Player>();
    }

    private IEnumerator Despawnskeleton(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(skeletonGameObject);
    }

    void Update()
    {
        if (skeletonStats.isDead)
        {
            if (navMeshAgent.enabled)
            {
                try
                {
                    navMeshAgent.isStopped = true;
                }
                catch (InvalidOperationException) { }
                navMeshAgent.enabled = false;
            }

            if (skeletonCollider.enabled)
            {
                skeletonCollider.enabled = false;
            }

            startDespawnTimer = true;  

            if (startDespawnTimer)
            {
                timeSinceDeath += Time.deltaTime;
                if (timeSinceDeath >= despawnDelay)
                {
                    Destroy(skeletonGameObject);
                }
            }

            return;
        }

        timeSinceLastAttack += Time.deltaTime;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(playerTransform.position, out hit, 0.1f, NavMesh.AllAreas))
        {
            float distanceToPlayer = Vector3.Distance(transform.position, hit.position);
            playerInSightRange = distanceToPlayer <= sightRange;
        }
        else
        {
            playerInSightRange = false;
        }

        bool isReturning = false;

        if (playerInSightRange)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

            if (distanceToPlayer > attackDistance)
            {
                navMeshAgent.SetDestination(playerTransform.position);
                navMeshAgent.isStopped = false;
            }
            else
            {
                navMeshAgent.isStopped = true;
            }

            if (timeSinceLastAttack >= attackCooldown && distanceToPlayer <= attackDistance)
            {
                timeSinceLastAttack = 0f;
                int attackNumber;

                do
                {
                    attackNumber = UnityEngine.Random.Range(1, numberOfAttacks + 1);
                } while (attackNumber == previousAttackNumber);

                skeletonAnimator.SetTrigger("Attack" + attackNumber);
                previousAttackNumber = attackNumber;
            }

            skeletonAnimator.SetBool("Returning", false);
        }
        else
        {
           Patrol();
        }

        if (isReturning)
        {
            Vector3 direction = (originalPosition - transform.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            float rotationSpeed = 3f;
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    void Patrol()
    {
        if (waypoints.Length == 0) return;

        navMeshAgent.SetDestination(waypoints[currentWaypointIndex].position);
        navMeshAgent.isStopped = false;

        if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position) < 0.5f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }

        skeletonAnimator.SetBool("Returning", false);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
