using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Projectile
{
    public void Start()
    {
        SetMoveDirection(Vector2.up);
    }
}
