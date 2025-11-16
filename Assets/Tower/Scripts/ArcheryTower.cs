using UnityEngine;

public class ArcheryTower : Tower
{
    [SerializeField] GameObject bulletPrefab;
    float yOffset = 0f;

    public override void Attack(GameObject target, int damage, float speed)
    {
        GameObject bullet = Instantiate(bulletPrefab, transform);
        bullet.transform.position = transform.position + Vector3.up * yOffset;
        bullet.GetComponent<Bullet>().Shoot(target, damage, speed);
    }

    new void Awake()
    {
        base.Awake();
        yOffset = 4.8f * transform.localScale.y;
    }
}
