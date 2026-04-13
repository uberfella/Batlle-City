using UnityEngine;

public class ShellPlayer : Shell
{
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            return;



        base.OnTriggerEnter2D(other);
    }

    protected override void Explode()
    {
        Vector2 explosionCenter = transform.position;
        Vector2 explosionSize = new Vector2(1.0f, 0.25f); // 2.0f left, 2.0f right, 0.5f forward
        Collider2D[] objectsHit = Physics2D.OverlapBoxAll(explosionCenter, explosionSize, transform.eulerAngles.z);

        Instantiate(explosionEffectPrefab, transform.position, transform.rotation);

        foreach (Collider2D obj in objectsHit)
        {   
            //if (obj.name.Contains("Concr") && PlayerProperties.playerLevel < 3)
            //{
            //    continue;
            //}
            Debug.Log("obj = " + obj);
            //obj = Concrete_quad(1)(UnityEngine.BoxCollider2D)
            if (obj.GetComponent<IDamageable>() != null)
            {
                IDamageable target = obj.GetComponent<IDamageable>();
                target.TakeDamage(1, this);
            }
            if (obj.GetComponent<IDamageableEnemy>() != null)
            {
                IDamageableEnemy target = obj.GetComponent<IDamageableEnemy>();
                target.TakeDamage(1);
            }
        }
    }

}
