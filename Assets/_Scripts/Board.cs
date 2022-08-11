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
        EventsManager.OnGameStateChange += GameStateChanged;
    }

    private void OnDestroy()
    {
        EventsManager.OnPowerupPickup -= PowerupPickup;
        EventsManager.OnGameStateChange -= GameStateChanged;
    }

    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
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

    void GameStateChanged(GameState state)
    {
        switch (state)
        {
            case GameState.GameStart:
                Spawner.CreateOnTemplate<Ball>(ballPrefab, spawnPoint.position);
                break;

            case GameState.GameLoad:
                Spawner.CreateOnTemplate<Ball>(ballPrefab, spawnPoint.position);
                break;

            case GameState.Win:
                WaitAncCreateBall(1f);
                break;

            case GameState.BallLose:
                if (GameManager.HP > 0) Spawner.CreateOnTemplate<Ball>(ballPrefab, spawnPoint.position);
                break;

                // case GameState.Pause:
                //     break;

                // case GameState.Restore:
                //     break;
        }
    }

    void WaitAncCreateBall(float timer)
    {
        StartCoroutine(Wait());

        IEnumerator Wait()
        {
            yield return new WaitForSeconds(timer);
            Spawner.CreateOnTemplate<Ball>(ballPrefab, spawnPoint.position);
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
                    Spawner.CreateOnTemplate<Bullet>(bulletPrefab, spawnPoint.position);
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
                    yield return new WaitForSeconds(0.5f);
                    Spawner.CreateOnTemplate<Ball>(ballPrefab, spawnPoint.position);
                }
            }
        }
    }
}
