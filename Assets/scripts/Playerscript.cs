//The script was derived from the Unity2D Tutorial at http://pixelnest.io/tutorials/2d-game-unity/background-and-camera/

using UnityEngine;


public class Playerscript : MonoBehaviour {

    // This declares the public variable of the default speed of the player ship
    public Vector2 speed = new Vector2(50, 50);

    // Declare the movement of the player as well as the rigidbody linked to the player ship graphic
    private Vector2 movement;
    private Rigidbody2D rigidbodyComponent;
    private HealthScript getHP;

    void Update()
    {
        // This function basically retrieves the keyboard input of the user that allows the player ship to move around
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        // Based on this input, the speed and direction of the ship is updated as the game progresses
        movement = new Vector2(
          speed.x * inputX,
          speed.y * inputY);

        // This allows the user to shoot the bullet based on input
        //  In Unity, Fire1 is Ctrl key and Fire2 is Alt key
        bool shoot = Input.GetButtonDown("Fire1");
        shoot |= Input.GetButtonDown("Fire2");
        

        if (shoot)
        {
            // If the CTRL or ALT is pressed, the player ship obtains the weapon
            WeaponScript weapon = GetComponent<WeaponScript>();
            if (weapon != null)
            {
                // Attack with the weapon script, though the value is false so that the
                // bullet does not hurt the player
                weapon.Attack(false);
                // Activate the sound
                SoundEffectsHelper.Instance.MakePlayerShotSound();
            }
        }

        // This code is based on the pixelnest tutorial: http://pixelnest.io/tutorials/2d-game-unity/parallax-scrolling/
        //   the following code restricts the player movement within the camera zone
        var dist = (transform.position - Camera.main.transform.position).z;

        var leftBorder = Camera.main.ViewportToWorldPoint(
          new Vector3(0, 0, dist)
        ).x;

        var rightBorder = Camera.main.ViewportToWorldPoint(
          new Vector3(1, 0, dist)
        ).x;

        var topBorder = Camera.main.ViewportToWorldPoint(
          new Vector3(0, 0, dist)
        ).y;

        var bottomBorder = Camera.main.ViewportToWorldPoint(
          new Vector3(0, 1, dist)
        ).y;

        transform.position = new Vector3(
          Mathf.Clamp(transform.position.x, leftBorder, rightBorder),
          Mathf.Clamp(transform.position.y, topBorder, bottomBorder),
          transform.position.z
        );

        getHP = this.GetComponent<HealthScript>();
        getHP.UpdateScore();
    }

    void FixedUpdate()
    {
        // Get the component and store the reference
        if (rigidbodyComponent == null) rigidbodyComponent = GetComponent<Rigidbody2D>();

        // Apply the movement values so that player graphic can move through the rigidbody component
        rigidbodyComponent.velocity = movement;
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        //Code for power up
        bool boostPlayer = false;
        int bonusHP = 0;
        float bonusSpeed = 0;
        
        bool damagePlayer = false;
        int hurt = 0;

        PowerUpScript powerUp = collision.gameObject.GetComponent<PowerUpScript>();
        if (powerUp != null)
        {
            bonusHP = powerUp.getHealth();
            bonusSpeed = powerUp.getSpeed();
            powerUp.vanish();
            boostPlayer = true;
        }

        //Increase player ship's capability
        if(boostPlayer)
        {
            if (bonusHP != 0)
            {
                HealthScript playerHealth = this.GetComponent<HealthScript>();
                playerHealth.AddHP(bonusHP);
                SoundEffectsHelper.Instance.MakeHealthUpgradeSound();
            }

            if (bonusSpeed != 0)
            {
                speed.x += bonusSpeed;
                speed.y += bonusSpeed;
                SoundEffectsHelper.Instance.MakeSpeedUpgradeSound();
            }
            
        }

        // Collision with enemy
        EnemyScript enemy = collision.gameObject.GetComponent<EnemyScript>();
        if (enemy != null)
        {
            // Kill the enemy
            HealthScript enemyHealth = enemy.GetComponent<HealthScript>();
            
            if (enemyHealth != null) enemyHealth.Damage(enemyHealth.hp);

            hurt = 1;
            damagePlayer = true;
        }

        // Collision with enemy
        SmartEnemyScript smartEnemy = collision.gameObject.GetComponent<SmartEnemyScript>();
        if (smartEnemy != null)
        {
            // Kill the enemy
            MonsterHealthScript enemyHealth = smartEnemy.GetComponent<MonsterHealthScript>();
            
            if (enemyHealth != null) enemyHealth.Damage(enemyHealth.hp);

            hurt = 2;
            damagePlayer = true;
        }

        // Collision with Boss
        BossScript bossEnemy = collision.gameObject.GetComponent<BossScript>();
        if (smartEnemy != null)
        {
            //Colliding with boss does not hurt the boss
            hurt = 1;
            damagePlayer = true;
        }


        // Damage the player
        if (damagePlayer)
        {
            HealthScript playerHealth = this.GetComponent<HealthScript>();
            if (playerHealth != null) playerHealth.Damage(hurt);
        }
    } 
    
    void OnDestroy()
    {
        // Game Over.
        ScoreScript.Instance.updateHealth(0);
        var gameOver = FindObjectOfType<GameOverScript>();
        gameOver.ShowButtons();
    }
}
