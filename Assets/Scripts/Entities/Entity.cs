using System.Collections;
using UnityEngine;

public class Entity : MonoBehaviour
{
    // Data fields that all Entities (Player, Mobs) share
    public string entity_name;
    public string description;

    public int maxHP;
    public int currentHP;

    public int walkSpeed;

    public Entity(string name, string descr, int maxHealth)
    {
        entity_name = name;
        description = descr;
        maxHP = maxHealth;
    }

    public void Initialize()
    {
        currentHP = maxHP;
    }

    public virtual void TakeDamage(int damageAmount)
    {
        currentHP = currentHP - damageAmount;
        if (currentHP <= 0)
        {
            Die();
        }
        if (currentHP > maxHP)
        {
            currentHP = maxHP;
        }
    }

    protected virtual void Die()
    {
        // die
    }
}
