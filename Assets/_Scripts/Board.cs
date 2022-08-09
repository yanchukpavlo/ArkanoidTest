using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Board : MonoBehaviour
{
    [SerializeField] float speed = 10;
    [SerializeField] Transform spawnPoint;
    [SerializeField] Ball ballPrefab;
    [SerializeField] Bullet bulletPrefab;
    [SerializeField] float timeBetweenShot = 1f;
    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        EventsManager.OnPowerupPickup += PowerupPickup;
    }

    private void OnDestroy()
    {
        EventsManager.OnPowerupPickup -= PowerupPickup;
    }

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        if (horizontal != 0)
        {
            rb.AddForce(horizontal * speed * Vector3.right);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.TryGetComponent(out Ball ball))
        {
            Vector2 direction = ball.transform.position - transform.position;
            ball.SetMoveDirection(direction);
        }
    }

    void PowerupPickup(PowerupType type)
    {
        switch (type)
        {
            case PowerupType.Bullet:
                PickupBullet();
                break;

            case PowerupType.MultiBall:
                PickupMultiBall();
                break;
        }

        void PickupBullet()
        {
            StartCoroutine(Shot());

            IEnumerator Shot()
            {
                for (int i = 0; i < 3; i++)
                {
                    yield return new WaitForSeconds(timeBetweenShot);
                    CreateOnTemplate<Bullet>(bulletPrefab, spawnPoint.position);
                }
            }
        }

        void PickupMultiBall()
        {
            StartCoroutine(MultiBall());

            IEnumerator MultiBall()
            {
                for (int i = 0; i < 3; i++)
                {
                    yield return new WaitForSeconds(timeBetweenShot);
                    CreateOnTemplate<Ball>(ballPrefab, spawnPoint.position);
                }
            }
        }
    }

    T CreateOnTemplate<T>(T prefab, Transform parent, Vector3 position) where T : MonoBehaviour
    {
        T p = Instantiate(prefab, parent);
        p.transform.position = position;
        return p;
    }

    T CreateOnTemplate<T>(T prefab, Transform parent) where T : MonoBehaviour => CreateOnTemplate<T>(prefab, parent, Vector3.zero);
    T CreateOnTemplate<T>(T prefab, Vector3 position) where T : MonoBehaviour => CreateOnTemplate<T>(prefab, null, position);
    T CreateOnTemplate<T>(T prefab) where T : MonoBehaviour => CreateOnTemplate<T>(prefab, null, Vector3.zero);
}
