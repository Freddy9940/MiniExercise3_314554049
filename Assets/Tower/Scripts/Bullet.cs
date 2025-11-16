using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public enum BulletType
    {
        Arrow,
        CannonBall
    }
    [SerializeField] BulletType bulletType;
    [SerializeField] GameObject landingEffect;
    [SerializeField] GameObject model;
    [SerializeField] float yOffset = 0f;
    [SerializeField] ParticleSystem particles;
    Vector3 targetPosition = new(0, 0, 0);
    bool flying = false;
    bool AOE = false;
    float AOE_range = 1f;
    int damage = 10;
    float speed = 1f;
    float verticalSpeed = 0f;
    float gravity = -50f;
    GameObject target;

    public void Shoot(GameObject _target, int _damage, float _speed, bool isAOE=false, float range=1f)
    {
        target = _target;
        damage = _damage;
        speed = _speed;
        AOE = isAOE;
        AOE_range = range;

        targetPosition = target.transform.position + Vector3.up * yOffset;
        transform.LookAt(target.transform);
        flying = true;

        if (bulletType == BulletType.CannonBall)
        {
            float distance = (targetPosition - transform.position).magnitude;
            float travelTime = Mathf.Sqrt(transform.position.y * 2 / -gravity) * 1.2f;
            speed = distance / travelTime;
        }
    }

    void Land()
    {
        flying = false;

        GameObject effect = Instantiate(landingEffect);
        if (AOE)
        {
            effect.transform.localScale *= AOE_range;
        }
        effect.transform.position = targetPosition;

        DealDamage();
        if (particles)
        {
            var emission = particles.emission;
            emission.enabled = false;
        }
        model.SetActive(false);
        Debug.Log("Land");
        StartCoroutine(DelayedDestroy(1f));
    }

    IEnumerator DelayedDestroy(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }

    void DealDamage()
    {
        if (!AOE)
        {
            target.GetComponent<Goblin_AC>().TakeDamage(damage);
        }
        else
        {
            targetPosition.y = 0;
            GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach(GameObject enemy in allEnemies)
            {
                Vector3 enemyPosition = enemy.transform.position;
                enemyPosition.y = 0;
                float distance = (enemyPosition - targetPosition).magnitude;
                if (distance <= AOE_range)
                {
                    enemy.GetComponent<Goblin_AC>().TakeDamage(damage);
                }
            }
        }
    }

    void Update()
    {
        if (flying)
        {
            if (bulletType == BulletType.Arrow)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition,
                                                         speed * Time.deltaTime);
                if (transform.position == targetPosition) Land();
            }
            else if (bulletType == BulletType.CannonBall)
            {
                verticalSpeed += gravity * Time.deltaTime;

                Vector3 modifiedPos = targetPosition;
                modifiedPos.y = transform.position.y;
                transform.position = Vector3.MoveTowards(transform.position, modifiedPos,
                                                         speed * Time.deltaTime);
                transform.position += Vector3.up * verticalSpeed * Time.deltaTime;

                if (transform.position.y < targetPosition.y) Land();
            }
        }
    }
}
