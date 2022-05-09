using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public float speed;

    [Header("Launch")]
    public GameObject projectilePrefab;
    public AudioClip launchClip;

    private Vector3 moveInput;
    private Vector2 lookDirection = new Vector2(1, 0);

    private Rigidbody2D rb;
    private Animator animator;
    private AudioSource audioSource;

    private void Awake()
    {
        instance = this;

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        GetMovementInput();
        UpdateLookDirection();
        CheckInput();
        SetAnimationParameter();
    }
    private void FixedUpdate()
    {
        ApplyMovement();
    }
    private void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            Launch();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            RaycastNPC();
        }
    }
    private void GetMovementInput()
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

        PlaySound(launchClip);
        animator.SetTrigger("Launch");
    }
    private void RaycastNPC()
    {
        RaycastHit2D hit = Physics2D.Raycast(rb.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));

        if (hit.collider != null)
        {
            NPC npc = hit.collider.GetComponent<NPC>();

            if (npc != null)
            {
                npc.DisplayDialog();
            }
        }
    }
    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
