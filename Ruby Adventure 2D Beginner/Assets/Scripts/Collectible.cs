using UnityEngine;

public class Collectible : MonoBehaviour
{
    public GameObject particleEffect;
    public AudioClip audioClip;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CollectibleEffect();
        }
    }
    protected virtual void CollectibleEffect() { }
}
