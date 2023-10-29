using UnityEngine;

public class Mob : Entity
{
    public int damage;

    public Mob(string name, string description, int maxHP) : base(name, description, maxHP)
    {
        base.name = name;
        base.description = description;
        base.maxHP = maxHP;
    }
    private new void Initialize()
    {
        // base.Initialize(): "currentHP = maxHP;"
        base.Initialize(); 
    }
    

    protected override void Die()
    {
        // Implement mob-specific death behavior here
    }
}