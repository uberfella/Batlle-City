using System.Collections;
using UnityEngine;

public class ExplosionEffectScript : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite[] explosionSprites; // assign 3 sprites
    [SerializeField] private float frameTime = 0.1f;     // time per sprite
    void Start()
    {
        StartCoroutine(PlayExplosion());
    }

    IEnumerator PlayExplosion()
    {
        for (int i = 0; i < explosionSprites.Length; i++)
        {
            spriteRenderer.sprite = explosionSprites[i];
            yield return new WaitForSeconds(frameTime);
        }

        Destroy(gameObject);
    }
}
