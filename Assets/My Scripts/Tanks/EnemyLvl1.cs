using UnityEngine;

public class EnemyLvl1 : Enemy
{
    //public GameObject shellPrefab;

    private Rigidbody2D rb;
    //private Vector2 movement;
    //private bool isMovingHorizontally = true; // Flag to track if the player is moving horizontally

    void Start()
    {
        health = 1;
        speed = 5f;
        scoreOnDestroy = 1;
        

    }

    // Update is called once per frame
    void Update()
    {
        //ShootTheGun();
    }

    void FixedUpdate()
    {
        // Apply movement to the player in FixedUpdate for physics consistency
        //rb.linearVelocity = movement * speed;
    }

    
}
