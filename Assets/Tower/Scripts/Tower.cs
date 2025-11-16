using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Tower : MonoBehaviour
{
    [SerializeField] InputActionAsset InputActions;
    InputAction interactAction;
    [SerializeField] Transform scaler;
    [SerializeField] GameObject rangeIndicator;
    [SerializeField] int attackPower = 10;
    [SerializeField] float attackInterval = 1f;
    [SerializeField] float attackRange = 5f;
    // bool enchanted = false;
    float attackCooldown = 0f;
    bool rangeIndicator_showing = false;
    
    public abstract void Attack(GameObject target, int damage, float speed);

    // Update is called once per frame
    protected void Update()
    {
        attackCooldown -= Time.deltaTime;
        if (attackCooldown < 0) attackCooldown = 0;

        // Show range
        if (interactAction.WasPressedThisFrame())
        {
            // Debug.Log("key pressed");
            if (rangeIndicator_showing)
            {
                rangeIndicator.SetActive(false);
                rangeIndicator_showing = false;
            }
            else
            {
                rangeIndicator.transform.localScale = new(attackRange, 0.2f, attackRange);
                rangeIndicator.SetActive(true);
                rangeIndicator_showing = true;
            }
        }

        // Try to attack
        if (attackCooldown == 0)
        {
            GameObject enemy = SearchForEnemy();
            if (!enemy) return;
            Attack(enemy, attackPower, 25);
            attackCooldown = attackInterval;
        }
    }

    protected GameObject SearchForEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        List<GameObject> enemiesInRange = new();
        foreach (GameObject enemy in enemies)
        {
            if (enemy.GetComponent<Goblin_AC>().isDead) continue;
            Vector3 towerPosition = transform.position;
            Vector3 enemyPosition = enemy.transform.position;
            towerPosition.y = 0;
            enemyPosition.y = 0;
            if ((enemyPosition - towerPosition).magnitude < attackRange)
            {
                enemiesInRange.Add(enemy);
            }
        }
        if (enemiesInRange.Count == 0) return null;

        GameObject target = null;

        // TODO: choose an enemy
        target = enemiesInRange[Random.Range(0, enemiesInRange.Count)];

        return target;
    }

    void OnEnable()
    {
        InputActions.FindActionMap("Player").Enable();
    }

    void OnDisable()
    {
        InputActions.FindActionMap("Player").Disable();
    }

    protected void Awake()
    {
        interactAction = InputActions.FindAction("Interact");
    }

    void Start()
    {
        Vector3 parentScale = transform.localScale;
        scaler.localScale = new(1 / parentScale.x, 1 / parentScale.y, 1 / parentScale.z);
    }
}
