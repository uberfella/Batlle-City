using UnityEngine;

public class Shell : MonoBehaviour
{
    public float speed = 5f;
    private Vector2 movement; // Stores the direction of shell
    private float horizontalBounds = 7f;//-6 +7
    private float verticalBounds = 7f;//-6 +7


    void Start()
    {
        
    }

    void Update()
    {

        FlyForward();

        //destroy shells outside of the gaming field
        if (transform.position.x < (-horizontalBounds + 1) || transform.position.x > horizontalBounds ||
            transform.position.y < (-verticalBounds + 1) || transform.position.y > verticalBounds)
        {
            Destroy(gameObject);
        }

    }

    private void FlyForward()
    {
        transform.position += transform.up * speed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Brick") || 
            other.gameObject.CompareTag("Concrete") || 
            other.gameObject.CompareTag("Base"))
        {
            Destroy(gameObject);
        }
    }


}
