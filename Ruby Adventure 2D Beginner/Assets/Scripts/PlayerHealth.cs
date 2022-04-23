using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth instance;

    public int maxHealth = 5;

    [HideInInspector] public int currentHealth;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        currentHealth = maxHealth;   
    }
    public void ChangeHealth(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
    }
}
