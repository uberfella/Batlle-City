using UnityEngine;

public class Shell : MonoBehaviour
{
    public float speed = 5f;

    void Update()
    {
        FlyForward();
    }

    private void FlyForward()
    {
        transform.position += transform.up * speed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.gameObject.CompareTag("Brick") ||
            other.gameObject.CompareTag("Wall") ||
            other.gameObject.CompareTag("Base"))
        {
            Explode();
            Destroy(gameObject);
        }
    }

    private void Explode()
    {
        Vector2 explosionCenter = transform.position;
        Vector2 explosionSize = new Vector2(1.0f, 0.25f); // 2.0f left, 2.0f right, 0.5f forward
        Collider2D[] objectsHit = Physics2D.OverlapBoxAll(explosionCenter, explosionSize, transform.eulerAngles.z);

        foreach (Collider2D obj in objectsHit)
        {
            if (obj.CompareTag("Brick") || /*obj.CompareTag("Concrete") ||*/ obj.CompareTag("Base"))
            {
                Destroy(obj.gameObject);
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector2 explosionSize = new Vector2(1.0f, 0.25f);
        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);
        Gizmos.DrawWireCube(Vector3.zero, explosionSize);
    }
}
