using System.Collections;
using UnityEngine;

public class Entity : MonoBehaviour
{
    // Data fields that all Entities (Player, Mobs) share
    [SerializeField]
    public string entity_name;
    public string description;
    public float maxHP;
    public float currentHP;
    public float walkSpeed;


    public void Start()
    {
        currentHP = maxHP;
    }


    public virtual void TakeDamage(float damageAmount)
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
