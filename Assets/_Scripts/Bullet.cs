using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Projectile
{
    public void Start()
    {
        SetMoveDirection(Vector2.up);
    }

    protected override void InTriggerEnter(Collider other)
    {
        base.InTriggerEnter(other);

        if (other.TryGetComponentInRigidbody(out Block block))
        {
            block.TakeHit(damage);
            Destroy(gameObject);
        }
    }
}
