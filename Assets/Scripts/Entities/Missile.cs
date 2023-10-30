using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    // When a missile is created, it immediately has a default speed and damage
    // (from this class) and a direction (from the instantiating object).
    // Speed and damage values must be set immediately after instantiation.
    // Once instantiated, it needs no more handling -- if it hits an object, it will
    // call that object's appropriate method.
    public float Speed = 10f;
    public float lifespan = 2f;
    public float damage = 10f;
    private float timer = 0;


    public void SetSpeed(float newSpeed)
    {
        Speed = newSpeed;
    }
    public void SetDamage(float newDamage)
    {
        damage = newDamage;
    }


    // Update is called once per frame
    void Update()
    {
        // move
        transform.Translate(Vector3.forward * Speed * Time.deltaTime);

        // check if time is up
        timer += Time.deltaTime;
        if (timer >= lifespan)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider otherObject)
    {
        // Check for collisions

        if (otherObject.CompareTag("Enemy"))
        {
            Mob enemy = otherObject.GetComponent<Mob>();
            enemy.TakeDamage(damage);
        }
        if (otherObject.CompareTag("Environment"))
        {
            // handle environmental collision
        }

        // Destroy projectile
        Destroy(gameObject);
    }
}
