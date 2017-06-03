
//The script was derived from the Unity2D Tutorial at http://pixelnest.io/tutorials/2d-game-unity/background-and-camera/
using UnityEngine;

public class WeaponScript : MonoBehaviour {

    //This allows the designer to set the prefab sprite into the script
    public Transform shotPrefab;

    //This variable sets the shooting rate of the ship, which can be adjust by the designer to determine
    //  how fast or slow the ship is shooting
    public float shootingRate = 0.25f;

    // This is to make sure that none of the ships are shooting without any cooldowns. Otherwise, the ships will keep shooting
    private float shootCooldown;


    //Initiate the cooldown rate to 0
    void Start()
    {
        shootCooldown = 0f;
    }

    //If the bullet was shot and the cool down rate is more than 0, then reduce the cooldown rate based on delta Time.
    void Update()
    {
        if (shootCooldown > 0)
        {
            shootCooldown -= Time.deltaTime;
        }
    }

    

    // This function allows the bullet to travel across the screen and hurt the enemy ship
    public void Attack(bool isEnemy)
    {
        if (CanAttack)
        {
            
            shootCooldown = shootingRate;

            // Create a new shot
            var shotTransform = Instantiate(shotPrefab) as Transform;

            // Assign position
            shotTransform.position = transform.position;

            // The is enemy property
            ShotScript shot = shotTransform.gameObject.GetComponent<ShotScript>();
            if (shot != null)
            {
                shot.isEnemyShot = isEnemy;
            }

            // Make the weapon shot always towards it
            MoveScript move = shotTransform.gameObject.GetComponent<MoveScript>();
            if (move != null)
            {
                move.direction = this.transform.right; 
            }
        }
    }

    
    public bool CanAttack
    {
        get
        {
            return shootCooldown <= 0f;
        }
    }
}
