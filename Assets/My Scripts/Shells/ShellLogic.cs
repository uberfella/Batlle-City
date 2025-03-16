using UnityEngine;

public class Shell : MonoBehaviour
{
    public float speed = 0.1f;
    //public GameObject myPrefab;
    //private EnemyLvl1 enemyLvl1;

    void Start()
    {
        //enemyLvl1 = GetComponent<EnemyLvl1>();
    }
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



        //the shell is player's
        if (gameObject.CompareTag("ShellPlayer"))
        {
            if (other.gameObject.CompareTag("EnemyLvl1"))
            {
                EnemyLvl1 enemyLvl1 = other.gameObject.GetComponent<EnemyLvl1>();
                if (enemyLvl1 != null)
                {
                    enemyLvl1.TakeDamage(1);
                    Destroy(gameObject);
                }
            }
            //destroy both shells if they collide w each other
            else if (other.gameObject.CompareTag("ShellEnemy"))
            {
                Destroy(other.gameObject);
                Destroy(gameObject);
            }
        }
        //the shell is enemy's
        else if (gameObject.CompareTag("ShellEnemy"))
        {
            if (other.gameObject.CompareTag("Player"))
            {
                //Destroy(other.gameObject);
                Destroy(gameObject);
            }
            //else if (other.gameObject.CompareTag("ShellPlayer")) 
            //{
            //    Destroy(other.gameObject);
            //    Destroy(gameObject);
            //}

        }

        if (other.gameObject.CompareTag("Brick") ||
            other.gameObject.CompareTag("Wall") ||
            other.gameObject.CompareTag("Base") ||
            other.gameObject.CompareTag("Concrete"))
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

        //TODO optimize
        foreach (Collider2D obj in objectsHit)
        {
            //the shell is player's
            if (gameObject.CompareTag("ShellPlayer"))
            {
                if (obj.CompareTag("Brick") || /*obj.CompareTag("Concrete") ||*/ obj.CompareTag("Base"))
                {
                    Destroy(obj.gameObject);
                }
                else if (obj.CompareTag("EnemyLvl1"))
                {
                    EnemyLvl1 enemyLvl1 = obj.gameObject.GetComponent<EnemyLvl1>();
                    enemyLvl1.TakeDamage(1);
                }
            }
            else
            //the shell is enemy's
            if (gameObject.CompareTag("ShellEnemy"))
            {
                if (obj.CompareTag("Brick") || /*obj.CompareTag("Concrete") ||*/ obj.CompareTag("Base") || obj.CompareTag("Player"))
                {
                    Destroy(obj.gameObject);
                }

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
