using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public int Speed;
    public int Damage;
    public Vector3 Direction;
    public bool IsInMotion;

    // Constructors
    public void Initialize(int speed, int damage)
    {
        this.Speed = speed;
        this.Damage = damage;
        IsInMotion = false;
    }
    public void Initialize(Missile missile)
    {
        Speed = missile.Speed;
        Damage = missile.Damage;
        IsInMotion = false;
    }
    public void SetDirectionAndFire(Vector3 newDirection)
    {
        Direction = newDirection.normalized;
        IsInMotion = true;
    }
    // Update is called once per frame
    void Update()
    {
        /// ???
        if (IsInMotion)
        {
            transform.Translate(Direction * Speed * Time.deltaTime);
        }
    }
}
