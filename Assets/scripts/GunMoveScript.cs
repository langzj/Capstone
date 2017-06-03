//The script was derived from the Unity2D Tutorial at http://pixelnest.io/tutorials/2d-game-unity/background-and-camera/

using UnityEngine;


public class GunMoveScript : MonoBehaviour
{

    //Invokes the movement vector object with the default speed at 10 and 10
    //  since this is a public variable, it can be adjusted by the game designer
    public Vector2 speed = new Vector2(10, 10);

    // This is the direction to which the character will be moving
    //  -1 will mean that the ship will move from right to left
    public Vector2 direction /*= new Vector2(-1, 0)*/;

    // Declare private variables on the rigidbody component which is the movement/gravity engine component of the ship
    //  and another Vector object. Note that Vector2 is a native Unity object.
    private Vector2 movement;
    private Rigidbody2D rigidbodyComponent;
    private float timer;


    // Update is a native function of the Unity engine, which will refresh at each frame of the game while it's running
    void Update()
    {
        timer += Time.deltaTime;

        if (timer < 1)
        {
            direction = new Vector2(0, 0);
        } else if (timer > 1 && timer < 1.8)
        {
            direction = new Vector2(0, 1);
        }
        else if (timer > 1.8 && timer < 2.58)
        {
            direction = new Vector2(0, -1);
        }
        else
        {
            timer = 1;
        }

        // This function will continuously update the ship based on each frame of the game
        movement = new Vector2(
          speed.x * direction.x,
          speed.y * direction.y);
    }


    void FixedUpdate()
    {
        // The rigidbody component is basically an object that is linked to the sprite (graphic) of the ship
        //   This allows the developer to dictate interesting physics, including applying gravity and movement
        if (rigidbodyComponent == null) rigidbodyComponent = GetComponent<Rigidbody2D>();

        // Apply movement to the rigidbody
        rigidbodyComponent.velocity = movement;
    }
}