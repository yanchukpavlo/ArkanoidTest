using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [field: SerializeField] public float speed { get; private set; } = 10f;

    Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        SetMoveDirection(new Vector2(0.5f, 0.5f));
    }

    public void SetMoveDirection(Vector2 direction)
    {
        direction = direction.normalized;
        rb.velocity = direction * speed;
    }
}
