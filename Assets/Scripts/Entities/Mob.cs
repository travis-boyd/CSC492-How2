using UnityEngine;

public class Mob : Entity
{
    public int damage;

    private void Initialize()
    {
        // base.Initialize(): "currentHP = maxHP;"
        base.Initialize(); 
    }

    protected override void Die()
    {
        // Implement mob-specific death behavior here
    }
}