using UnityEngine;

public class Tank : MonoBehaviour
{

    public int health;
    public float speed;
    public Vector2 minBounds = new Vector2(-62f, 72f); // -6 -6 7 7
    public Vector2 maxBounds = new Vector2(61f, 71f);

    public virtual void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }


}
