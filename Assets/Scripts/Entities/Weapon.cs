using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public string weaponName;
    public Missile equippedMissile;

    private void Start()
    {
        weaponName = name;
    }

    //Change Missiles
    public void EquipMissile(Missile missile)
    {
        equippedMissile = missile;
    }

    public void Fire()
    {
        if (equippedMissile != null)
        {
            // Instantiate the missile and set its speed and direction
            Missile missileInstance = Instantiate(equippedMissile, transform.position, Quaternion.identity);
        }
        else
        {
            Debug.LogError("Fire() error: Weapon tried to Fire, but doesn't have a Missile.");
        }
    }

}
