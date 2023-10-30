using UnityEngine;
using UnityEngine.UI;

public class Player : Entity
{
    public float damage;
    public Slider healthBarSlider;
    public Weapon equippedWeapon;
    


    private new void Start()
    {
        // base.Initialize(): "currentHP = maxHP;"
        base.Start();
        Initialize();
    }
    private void Initialize()
    {
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

    public void FireWeapon()
    {
        if (equippedWeapon == null)
        {
            Debug.LogError("FireWeapon() error: No weapon equpped.");
        }
        else
        {
            equippedWeapon.Fire();
        }
    }

    public override void TakeDamage(float damage)
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
        base.Die();
        healthBarSlider.value = 0;
    }
}