using UnityEngine;

public class CannonTower : Tower
{
    [SerializeField] Transform cannonTrans;
    [SerializeField] float AOE_range = 2f;
    [SerializeField] GameObject bulletPrefab;
    float yOffset = 0f;
    // [SerializeField] float rotateSpeed = 1f;
    Transform targetTrans = null;

    new void Update()
    {
        if (!targetTrans || targetTrans.GetComponent<Goblin_AC>().isDead)
        {
            GameObject enemy = SearchForEnemy();
            if (enemy) targetTrans = enemy.transform;
        }

        if (targetTrans)
        {
            Vector3 targetPos = targetTrans.position;
            targetPos.y = cannonTrans.position.y;
            // TODO: smooth transition
            cannonTrans.LookAt(targetPos);
        }

        base.Update();
    }

    public override void Attack(GameObject target, int damage, float speed)
    {
        GameObject bullet = Instantiate(bulletPrefab, transform);
        bullet.transform.position = transform.position + Vector3.up * yOffset;
        bullet.GetComponent<Bullet>().Shoot(targetTrans.gameObject, damage, speed, true, AOE_range);
    }

    new void Awake()
    {
        base.Awake();
        yOffset = 1.6f * transform.localScale.y;
    }
}
