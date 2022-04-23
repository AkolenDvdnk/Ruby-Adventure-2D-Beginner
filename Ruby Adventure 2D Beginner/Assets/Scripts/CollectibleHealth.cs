using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleHealth : Collectible
{
    public int healPower = 1;

    protected override void CollectibleEffect()
    {
        if (PlayerHealth.instance.currentHealth == PlayerHealth.instance.maxHealth)
            return;

        PlayerHealth.instance.ChangeHealth(healPower);

        Destroy(gameObject);
    }
}
