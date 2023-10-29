using UnityEngine;
using UnityEngine.UI;

public class Player : Entity
{
    public int damage;
    public Slider healthBarSlider;
    public Weapon equippedWeapon;
    
    //   Inherited data:
    // public string entity_name;
    // public string description;
    // public int maxHP;
    // public int currentHP;
    // public int walkSpeed;

    public Player(string name, string description, int maxHP) : base(name, description, maxHP)
    {
        base.name = name;
        base.description = description;
        base.maxHP = maxHP;
    }
    private new void Initialize()
    {
        // base.Initialize(): "currentHP = maxHP;"
        base.Initialize();

        // GUI Healthbar setup:
        if (healthBarSlider != null)
        {
            healthBarSlider.maxValue = maxHP;
            healthBarSlider.value = currentHP;
        }
    }
    
    public void EquipWeapon(Weapon weapon)
    {
        equippedWeapon = weapon;
    }

    public override void TakeDamage(int damage)
    {
        // Update GUI
        if (currentHP > damage)
        {
            healthBarSlider.value -= damage;
        }

        base.TakeDamage(damage);
    }

    protected override void Die()
    {
        // Implement player-specific death behavior here

        healthBarSlider.value = 0;
    }
}