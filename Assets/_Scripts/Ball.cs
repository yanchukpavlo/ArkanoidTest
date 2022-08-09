using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Ball : MonoBehaviour
{
    [field: SerializeField] public float speed { get; private set; } = 10f;
    [field: SerializeField] public byte damage { get; private set; } = 1;

    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        InAwake();
    }

    private void OnCollisionEnter(Collision other) => InCollisionEnter(other);

    public virtual void InAwake()
    {
        SetMoveDirection(new Vector2(Random.Range(-0.5f, 0.5f), 1));        
    }

    public virtual void InCollisionEnter(Collision other)
    {
        if (other.TryGetComponentInRigidbody(out Block block))
        {
            block.TakeHit(damage);
        }
        else if (other.TryGetComponent<Zone>(out Zone zone))
        {
            ZoneInteract();
        }  
    }

    public void SetMoveDirection(Vector2 direction)
    {
        direction = direction.normalized;
        rb.velocity = direction * speed;
    }

    public void ZoneInteract()
    {

    }
}
