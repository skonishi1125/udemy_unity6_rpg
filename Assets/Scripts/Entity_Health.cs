using UnityEngine;

public class Entity_Health : MonoBehaviour
{
    private Entity_VFX entityVfx;

    [SerializeField] protected float maxHp = 100;
    [SerializeField] protected bool isDead;

    [Header("On Damage Knockback")]
    [SerializeField] private Vector2 knockbackPower;
    [SerializeField] private float knockbackDuration;


    protected virtual void Awake()
    {
        entityVfx = GetComponent<Entity_VFX>();
    }

    public virtual void TakeDamage(float damage, Transform damageDealer)
    {
        if (isDead)
            return;

        entityVfx?.PlayOnDamageVfx();
        ReduceHp(damage);

    }

    protected void ReduceHp(float damage)
    {
        maxHp -= damage;

        if (maxHp < 0)
            Die();

    }

    protected void Die()
    {
        isDead = true;
        Debug.Log("Entity died!");
    }


}
