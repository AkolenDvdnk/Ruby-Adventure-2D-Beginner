using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        DestroyTimer();
    }
    public void Launch(Vector2 direction, float force)
    {
        rb.AddForce(direction * force);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        EnemyController enemy = other.collider.GetComponent<EnemyController>();

        if (enemy != null)
        {
            enemy.Fix();
        }

        Destroy(gameObject);
    }
    private void DestroyTimer()
    {
        Destroy(gameObject, 3f);
    }
}
