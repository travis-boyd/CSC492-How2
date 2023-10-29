using UnityEngine;

public class Player : Entity
{
    public int damage;

    private new void Start()
    {
        // base.Start(): "currentHP = maxHP;"
        base.Start(); 
    }

    protected override void Die()
    {
        // Implement player-specific death behavior here
    }
}