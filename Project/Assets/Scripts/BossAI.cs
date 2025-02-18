using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;
using System;

public class BossAI : MonoBehaviour
{
    public GameObject bossGameObject;
    public float attackCooldown = 2f;
    public float attackDistance = 3f;
    public Transform playerTransform;
    public float returnSpeed = 1f;

    private Animator bossAnimator;
    private BossStats bossStats;
    private float timeSinceLastAttack = 0f;
    private int numberOfAttacks = 4;
    private int previousAttackNumber = 0;
    private NavMeshAgent navMeshAgent;

    public int healthRegenAmount = 1;
    private float healthRegenCooldown = 1f;
    private float timeSinceLastRegen = 0f;

    public float despawnDelay = 60f;
    private float timeSinceDeath = 0f;
    private bool startDespawnTimer = false;
    private Collider bossCollider;

    public float sightRange;
    public bool playerInSightRange;
    public BossHealthBar healthBar;
    public TextMeshProUGUI bossNameText;

    private Vector3 originalPosition;

    void Start()
    {
        bossAnimator = bossGameObject.GetComponent<Animator>();
        bossStats = bossGameObject.GetComponent<BossStats>();
        navMeshAgent = bossGameObject.GetComponent<NavMeshAgent>();
        bossCollider = bossGameObject.GetComponent<Collider>();

        originalPosition = transform.position;
        healthBar.gameObject.SetActive(false);

        Player player = FindObjectOfType<Player>();
        if (player != null)
        {
            player.RegisterBossDeath(bossStats);
        }
    }

    private IEnumerator DespawnBoss(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(bossGameObject);
    }

    void Update()
    {
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
            healthBar.gameObject.SetActive(false);
            bossNameText.gameObject.SetActive(false);

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
        timeSinceLastRegen += Time.deltaTime;

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

        healthBar.gameObject.SetActive(playerInSightRange);
        bossNameText.gameObject.SetActive(playerInSightRange);

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

            bossAnimator.SetBool("Returning", false);
        }
        else
        {
            navMeshAgent.isStopped = true;

            float step = returnSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, originalPosition, step);

            if (timeSinceLastRegen >= healthRegenCooldown)
            {
                timeSinceLastRegen = 0f;
                bossStats.RegenerateHealth(healthRegenAmount);
            }

            if (Vector3.Distance(transform.position, originalPosition) < 0.1f)
            {
                bossAnimator.SetBool("Returning", false);
            }
            else
            {
                bossAnimator.SetBool("Returning", true);
                isReturning = true;
            }
        }

        if (isReturning)
        {
            Vector3 direction = (originalPosition - transform.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            float rotationSpeed = 3f;
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
