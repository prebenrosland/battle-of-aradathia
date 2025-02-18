using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;
using AS;

public class MainBossAI : MonoBehaviour
{
    public GameObject bossGameObject;
    public float attackCooldown = 2f;
    public float attackDistance = 3f;
    public Transform playerTransform;

    public DamageCollider leftHandDamageCollider;
    public DamageCollider rightHandDamageCollider;

    private Animator bossAnimator;
    private MainBossStats bossStats;
    private float timeSinceLastAttack = 0f;
    private int numberOfAttacks = 4;
    private int previousAttackNumber = 0;
    private NavMeshAgent navMeshAgent;

    public float despawnDelay = 60f;
    private float timeSinceDeath = 0f;
    private bool startDespawnTimer = false;
    private Collider bossCollider;

    public float sightRange;
    public bool playerInSightRange;

    private void AssignPlayerTransform()
    {
        Player player = FindObjectOfType<Player>();
        if (player != null)
        {
            playerTransform = player.transform;
        }
    }

    private void OnEnable()
    {
        SceneManager.activeSceneChanged += OnActiveSceneChanged;
    }

    private void OnDisable()
    {
        SceneManager.activeSceneChanged -= OnActiveSceneChanged;
    }

    private void OnActiveSceneChanged(Scene previousScene, Scene currentScene)
    {
        AssignPlayerTransform();
    }
    
    void Start()
    {
        bossAnimator = bossGameObject.GetComponent<Animator>();
        bossStats = bossGameObject.GetComponent<MainBossStats>();
        navMeshAgent = bossGameObject.GetComponent<NavMeshAgent>();
        bossCollider = bossGameObject.GetComponent<Collider>();
    }

    private IEnumerator DespawnBoss(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(bossGameObject);
    }

    void Update()
    {
        if (playerTransform == null)
        {
            AssignPlayerTransform();
            if (playerTransform == null) return;
        }

        if (bossStats.isDead)
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

            if (bossCollider.enabled)
            {
                bossCollider.enabled = false;
            }

            startDespawnTimer = true;  

            if (startDespawnTimer)
            {
                timeSinceDeath += Time.deltaTime;
                if (timeSinceDeath >= despawnDelay)
                {
                    Destroy(bossGameObject);
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

                bossAnimator.SetTrigger("Attack" + attackNumber);
                previousAttackNumber = attackNumber;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}