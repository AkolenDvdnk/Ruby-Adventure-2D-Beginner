public class CollectibleHealth : Collectible
{
    public int healPower = 1;

    protected override void CollectibleEffect()
    {
        if (PlayerHealth.instance.currentHealth == PlayerHealth.instance.maxHealth)
            return;

        if (particleEffect != null)
        {
            Instantiate(particleEffect, transform.position, transform.rotation);
        }

        PlayerHealth.instance.ChangeHealth(healPower);

        Destroy(gameObject);
    }
}
