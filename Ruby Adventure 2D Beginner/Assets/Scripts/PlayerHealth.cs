using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth instance;

    public AudioClip clip;

    public int maxHealth = 5;
    [HideInInspector] public int currentHealth;

    public float timeInvincible = 2f;

    private float invincibleTimer;

    private bool isInvincible;

    private Animator animator;

    private void Awake()
    {
        instance = this;

        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        currentHealth = maxHealth;   
    }
    private void Update()
    {
        CheckInvincible();   
    }
    private void CheckInvincible()
    {
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;

            if (invincibleTimer < 0)
            {
                isInvincible = false;
            }
        }
    }
    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (isInvincible)
                return;

            PlayerController.instance.PlaySound(clip);
            animator.SetTrigger("Hit");

            isInvincible = true;
            invincibleTimer = timeInvincible;
        }

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);

        UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);
    }
}
