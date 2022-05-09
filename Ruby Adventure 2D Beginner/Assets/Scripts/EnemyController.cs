using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed;
    public float changeTime = 3f;

    public bool vertical;

    public ParticleSystem smokeEffect;
    public AudioClip audioFixed;

    private int direction = 1;

    private float timer;

    private bool broken = true;

    private Rigidbody2D rb;
    private Animator animator;
    private AudioSource audioSource;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        timer = changeTime;
    }
    private void Update()
    {
        if (!broken)
            return;

        UpdateTimer();
    }
    private void FixedUpdate()
    {
        if (!broken)
            return;

        MovePosition();
        SetAnimationParameter();
    }
    private void UpdateTimer()
    {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }
    }
    private void MovePosition()
    {
        Vector2 position = rb.position;

        if (vertical)
        {
            position.y = position.y + Time.deltaTime * speed * direction;
        }
        else
        {
            position.x = position.x + Time.deltaTime * speed * direction;
        }

        rb.MovePosition(position);
    }
    private void SetAnimationParameter()
    {
        if (vertical)
        {
            animator.SetFloat("Move X", 0);
            animator.SetFloat("Move Y", direction);
        }
        else
        {
            animator.SetFloat("Move X", direction);
            animator.SetFloat("Move Y", 0);
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerHealth.instance.ChangeHealth(-1);
        }
    }
    public void Fix()
    {
        broken = true;
        rb.simulated = false;

        audioSource.PlayOneShot(audioFixed);
        smokeEffect.Stop();
        animator.SetTrigger("Fixed");
    }
}
