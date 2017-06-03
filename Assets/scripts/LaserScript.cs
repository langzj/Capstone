using UnityEngine;


public class LaserScript : MonoBehaviour
{

    //Invokes the movement vector object with the default speed at 10 and 10
    //  since this is a public variable, it can be adjusted by the game designer
    public Vector2 speed = new Vector2(10, 10);

    
    // This is the direction to which the character will be moving
    //  -1 will mean that the ship will move from right to left
    public Vector2 direction = new Vector2(-1, 0);

    // Declare private variables on the rigidbody component which is the movement/gravity engine component of the ship
    //  and another Vector object. Note that Vector2 is a native Unity object.
    private Vector2 movement;
    private Rigidbody2D rigidbodyComponent;
    
    
    void Start()
    {
        InvokeRepeating("changeAngle", 0, 4);
    }


    // Update is a native function of the Unity engine, which will refresh at each frame of the game while it's running
    void Update()
    {
                        
        // This function will continuously update the ship based on each frame of the game
        movement = new Vector2(
          speed.x * direction.x,
          speed.y * direction.y);
    }

    //This allows the angle of the firing missile to change every 4 seconds
    void changeAngle()
    {
        direction = new Vector2(Random.Range(-2.0f, 0.5f), -1);
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
