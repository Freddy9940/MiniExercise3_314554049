using UnityEngine;
using FullOpaqueVFX;

public class MagicTower : Tower
{
    [SerializeField] VFX_SpellManager spellManager;
    
    public override void Attack(GameObject target, int damage, float speed)
    {
        spellManager.target = target.transform;
        spellManager.CastSpell(damage);
    }
}
