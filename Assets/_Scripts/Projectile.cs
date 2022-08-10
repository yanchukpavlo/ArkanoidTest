using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    [field: SerializeField] public float speed { get; private set; } = 10f;
    [field: SerializeField] public byte damage { get; private set; } = 1;

    protected Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void SetMoveDirection(Vector2 direction)
    {
        direction = direction.normalized;
        rb.velocity = direction * speed;
    }

    private void OnCollisionEnter(Collision other) => InCollisionEnter(other);

    protected virtual void InCollisionEnter(Collision other)
    {
        if (other.TryGetComponentInRigidbody(out Block block))
        {
            block.TakeHit(damage);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Zone>(out Zone zone))
        {
            ZoneInteract();
        }
    }

    protected virtual void ZoneInteract()
    {
        gameObject.SetActive(false);
    }
}
