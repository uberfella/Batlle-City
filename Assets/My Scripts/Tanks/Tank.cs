using UnityEngine;

public class Tank : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected BoxCollider2D boxCollider;
    protected int health;
    protected float speed;
    protected Vector2 minBounds = new Vector2(-6.5f, -7.5f);
    protected Vector2 maxBounds = new Vector2(6.5f, 6.5f);
    protected float horizontalInput;
    protected float verticalInput;
    public bool enemyIsAlive = false;

    //so in the google play ripoff there is obviously no diagonal movement just like in the NES original
    //but in the said ripoff if you hold vertical you can not change the direction to the horizontal, and if you hold horizontal you can change it to the vertical immediately
    //let's declare it in the superclass so the everyone can use it
    protected void RestrictDiagonalMovements()
    {
        if (verticalInput != 0)
        {
            horizontalInput = 0;
        }
        else if (horizontalInput != 0)
        {
            verticalInput = 0;
        }
    }

    protected void RotatePlayer(float x, float y)
    {
        // If there is no input, do not rotate the player
        if (x == 0 && y == 0) return;

        // Calculate the rotation angle based on input direction
        float angle = Mathf.Atan2(y, x) * Mathf.Rad2Deg;

        // Apply the rotation to the player
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    //public int GetLayerNumber() 
    //{

    //}

}
