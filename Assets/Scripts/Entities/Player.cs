using UnityEngine;

public class Player : Entity
{
    public int damage;
    public Slider healthBarSlider;
    
    //   Inherited data:
    // public string entity_name;
    // public string description;
    // public int maxHP;
    // public int currentHP;
    // public int walkSpeed;

    private new void Initialize()
    {
        // base.Initialize(): "currentHP = maxHP;"
        base.Initialize();
    }

    protected override void Die()
    {
        // Implement player-specific death behavior here
    }
}