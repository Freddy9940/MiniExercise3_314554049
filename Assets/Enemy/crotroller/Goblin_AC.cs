using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem; 

[RequireComponent(typeof(NavMeshAgent))]
public class Goblin_AC : MonoBehaviour 
{
    private NavMeshAgent agent;
    private Animator animator;

    private LevelManager levelManager;
    private HUDController HUDController;

    private string levelDestination;

    public Transform target;
    public int damage; 
    public int health = 10;
    public float attackRange = 1.5f; 
    public float turnSpeed = 10f; 
    public int goldValue = 5; 
    public GameObject coinPrefab; 

    public bool isDead = false;

    void Awake()
    {
        //find levelManager & UIManager
        if (levelManager == null)
        {
            levelManager = FindFirstObjectByType<LevelManager>();
            levelDestination = levelManager.levelDestination;
        }
        if (HUDController == null)
        {
            HUDController = FindFirstObjectByType<HUDController>();
        }
        //find target after init
        if (target == null)
        {
            target = GameObject.Find(levelDestination)?.transform;
        }


    }

  void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        agent.stoppingDistance = attackRange;
        agent.updatePosition = true;
        agent.updateRotation = false;

    }

    void Update()
    {
        if (isDead) return;
        if (target != null)
        {
            float distanceToTarget = Vector3.Distance(transform.position, target.position);

            if (distanceToTarget > agent.stoppingDistance)
            {
                agent.isStopped = false; 
                agent.SetDestination(target.position);
                FaceVelocity(); 
            }
            else
            {
                agent.isStopped = true;
                FaceTarget(); 
                animator.SetTrigger("Attack");
            }
        }
        else
        {
            agent.isStopped = true;
        }

        Vector3 localVelocity = transform.InverseTransformDirection(agent.velocity);
        float forwardSpeed = localVelocity.z;
        animator.SetFloat("ForwardSpeed", forwardSpeed);

        if (Keyboard.current != null && Keyboard.current.kKey.wasPressedThisFrame)
        {
            TakeDamage(5);
        }

        //After monster is reached dest playerhealth-monster damage
        if(agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
        {
            levelManager.playerHP -= damage;  
            HUDController.UpdateHPText();
            Debug.Log("playerHP remain :" + levelManager.playerHP);
            Destroy(gameObject);                       
        }
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }


    void FaceVelocity()
    {
        if (agent.velocity.sqrMagnitude > 0.01f) 
        {
            Quaternion lookRotation = Quaternion.LookRotation(agent.velocity.normalized);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return; 

        health -= damage;
        Debug.Log("Goblin Take" + damage + " damage, remain" + health + " health");

        if (health <= 0)
        {
            Die();
        }
    }

   
    private void Die()
    {
        isDead = true;
        Debug.Log("Goblin Died");
        agent.isStopped = true;
        agent.enabled = false;
        animator.SetTrigger("Die");
        Collider col = GetComponent<Collider>();
        if (col != null)
        {
            col.enabled = false;
        }

        levelManager.AddGold(goldValue);
        HUDController.UpdateGoldText();

        if (coinPrefab != null)
        {
            Instantiate(coinPrefab, transform.position, Quaternion.identity);
        }
        Destroy(gameObject, 3.0f);
    }
}