using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerupType
{
    Bullet,
    MultiBall
}

[RequireComponent(typeof(Rigidbody))]
public class Powerup : MonoBehaviour
{
    [field: SerializeField] public float speed { get; private set; } = 5f;
    [field: SerializeField] public PowerupType powerupType { get; private set; }

    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        powerupType = powerupType.RandomEnum<PowerupType>();
    }

    private void OnEnable()
    {
        Move();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponentInRigidbody(out Board ball))
        {
            Pickup();
        }
        else if (other.TryGetComponent<Zone>(out Zone zone))
        {
            ZoneInteract();
        }
    }

    void Move()
    {
        rb.velocity = speed * Vector3.down;
    }

    public void Pickup()
    {
        EventsManager.PowerupPickup(powerupType);
        Destroy(gameObject);
    }    

    public void ZoneInteract()
    {
        Destroy(gameObject);
    }
}
