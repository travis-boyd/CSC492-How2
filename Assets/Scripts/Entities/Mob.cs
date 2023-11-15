using UnityEngine;

public class Mob : Entity
{
    public int damage;

    new public void Start()
    {
        base.Start();
        Initialize();
    }

    public void Initialize()
    {
        // Mob specific init
    }

    protected override void Die()
    {
        // Implement mob-specific death behavior here
    }
}