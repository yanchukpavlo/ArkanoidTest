using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Ball
{
    public override void InAwake()
    {
        SetMoveDirection(Vector2.up);
    }
}
