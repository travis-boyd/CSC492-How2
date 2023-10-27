using UnityEngine;

public class Mob : Entity
{
    public int damage;

    private new void Start()
    {
        // base.Start(): "currentHP = maxHP;"
        base.Start(); 
    }

    protected override void Die()
    {
        // Implement mob-specific death behavior here
    }
}