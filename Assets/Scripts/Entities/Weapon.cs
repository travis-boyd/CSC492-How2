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
        equipedMissile = missile;
    }

    public void Fire(Vector3 targetDirection)
    {
        if (equipedMissile != null)
        {
            // Instantiate the missile and set its speed and direction
            Missile missileInstance = Instantiate(equippedMissile, transform.position, Quaternion.identity);
            missileInstance.SetDirectionAndFire(targetDirection);
        }
        else
        {
            Debug.Error("Weapon tried to Fire, but doesn't have a Missile.");
        }
    }
}
