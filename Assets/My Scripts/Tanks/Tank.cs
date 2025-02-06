using UnityEngine;

public class Tank : MonoBehaviour
{

    public int health;
    public float speed;

    public virtual void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }


}
