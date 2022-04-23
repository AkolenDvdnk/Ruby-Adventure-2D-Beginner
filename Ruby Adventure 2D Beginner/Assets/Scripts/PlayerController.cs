using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public float speed;

    private Vector3 moveInput;

    private Rigidbody2D rb;

    private void Awake()
    {
        instance = this;

        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        CheckInput();
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
}
