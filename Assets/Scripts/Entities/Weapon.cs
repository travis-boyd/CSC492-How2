using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public string name;
    public Missile equipedMissile;

    //Change Missiles
    public void EquipMissile(Missile missile)
    {
        equippedMissile = missile;
    }

    public void Fire(Vector3 targetDirection)
    {
        if (equippedMissile != null)
        {
            // Instantiate the missile and set its speed and direction
            Missile missileInstance = Instantiate(equippedMissile, transform.position, Quaternion.identity);
            missileInstance.SetDirectionAndFire(targetDirection);
        }
        else
        {
            Debug.LogError("Weapon tried to Fire, but doesn't have a Missile.");
        }
    }
}
