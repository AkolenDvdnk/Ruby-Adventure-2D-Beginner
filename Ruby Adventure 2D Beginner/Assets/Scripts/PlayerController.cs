using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public float speed;

    public GameObject projectilePrefab;

    private Vector3 moveInput;
    private Vector2 lookDirection = new Vector2(1, 0);

    private Rigidbody2D rb;
    private Animator animator;

    private void Awake()
    {
        instance = this;

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        CheckInput();
        UpdateLookDirection();
        UpdateInput();
        SetAnimationParameter();
    }
    private void FixedUpdate()
    {
        ApplyMovement();
    }
    private void CheckInput()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput.Normalize();
    }
    private void ApplyMovement()
    {
        rb.velocity = moveInput * speed;
    }
    private void UpdateLookDirection()
    {
        Vector2 move = new Vector2(moveInput.x, moveInput.y);

        if (!Mathf.Approximately(move.x, 0f) || !Mathf.Approximately(move.y, 0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }
    }
    private void SetAnimationParameter()
    {
        Vector2 move = new Vector2(moveInput.x, moveInput.y);

        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);
    }
    private void Launch()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, rb.position + Vector2.up * 0.5f, Quaternion.identity);

        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(lookDirection, 300f);

        animator.SetTrigger("Launch");
    }
    private void UpdateInput()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            Launch();
        }
    }
}
