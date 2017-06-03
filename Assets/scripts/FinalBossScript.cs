using UnityEngine;
using System.Collections;

public class FinalBossScript : MonoBehaviour {

    //The following private variables won't show in the Unity Engine UI
    //  this is how C# works in tandem with the unity engine
    private bool hasSpawn;
    //private BossMoveScript moveScript;
    private WeaponScript[] weapons;
    private WeaponScript[] lasers;

    private Collider2D coliderComponent;
    private SpriteRenderer rendererComponent;
    private ScrollingScript scrollingScript;
    private GunMoveScript gunMoveScript;
    private float timer;
    private GameObject gun;
    private GameObject shield;
    private GameObject shieldGenerator;
    private GameObject mainBody;
    private GameObject brokenGen;
    private GameObject ship;
    private GameObject pole;
    private HealthScript bodyHealth;
    private bool shieldExists;

    //Awake basically means that this function will run, no matter when the game started.
    //  We don't even need to call this function to have it activated
    void Awake()
    {
        
        gun = this.transform.Find("Gun").gameObject;
        weapons = gun.GetComponentsInChildren<WeaponScript>();
        gunMoveScript = gun.GetComponent<GunMoveScript>();


        ship = this.transform.Find("Top").gameObject;
        lasers = ship.GetComponentsInChildren<WeaponScript>();

        pole = this.transform.Find("Bottom").gameObject;
        shield = this.transform.Find("Shield").gameObject;
        shieldGenerator = this.transform.Find("ShieldGenerator").gameObject;
        mainBody = this.transform.Find("Body").gameObject;
        bodyHealth = mainBody.GetComponent<HealthScript>();
        //brokenGen = this.transform.Find("BrokenGenerator").gameObject;

        scrollingScript = GetComponentInParent<ScrollingScript>();
        

        //This allows both collider and renderer to be invoked
        // which involes the physics engine componenet of Unity2D
        coliderComponent = mainBody.GetComponent<Collider2D>();
        // This allows the enemy planes to start moving and shooting only when
        //   the camera arrives into where the enemy plane is located.
        rendererComponent = GetComponent<SpriteRenderer>();
    }

    // Start function is invoked when the game starts.
    void Start()
    {
        //We have disabled all the functions of the enemy character at start of the game
        //  so that they will only appear when the camera view arrives at where the enemy character is located.
        hasSpawn = false;

        //Deactive collider and the health script of the body until the shield is destroyed
        coliderComponent.enabled = false;
        bodyHealth.enabled = false;
        gunMoveScript.enabled = false;

        shieldExists = true;

        foreach (WeaponScript weapon in weapons)
        {
            weapon.enabled = false;
        }

        foreach (WeaponScript laser in lasers)
        {
            laser.enabled = false;
        }
    }

    void Update()
    {
        if(shieldGenerator == null && shieldExists)
        {
            SpecialEffectsHelper.Instance.Explosion(shield.transform.position);
            Destroy(shield);
            coliderComponent.enabled = true;
            bodyHealth.enabled = true;

            shieldExists = false;
        }

        if(mainBody == null)
        {
            SpecialEffectsHelper.Instance.BigExplosion(ship.transform.position);
            SpecialEffectsHelper.Instance.BigExplosion(pole.transform.position);
            SoundEffectsHelper.Instance.MakeExplosionSound();
            Destroy(gameObject);
            ScoreScript.Instance.AddScore(4000);

            /* foreach (Transform child in transform)
            {
                GameObject.Destroy(child.gameObject);
            }*/
        }
        

        // This allows the enemy to spawn once the enemy character is visible on the camera
        if (hasSpawn == false)
        {
            if (rendererComponent.IsVisibleFrom(Camera.main))
            {
                Spawn();
            }
        }
        else
        {

            timer += Time.deltaTime;

            if (timer > 5.7)
            {
                scrollingScript.enabled = false;
            }
            // Once spawned, the enemy will start shooting at the main character
            //  Goes through each child weapon script and start shooting those weapons
            foreach (WeaponScript weapon in weapons)
            {
                if (weapon != null && weapon.enabled && weapon.CanAttack)
                {
                    // True --> the bullets will hurt the player
                    // False --> the bullets will hurt the enemy
                    weapon.Attack(true);
                    // Sound initiated
                    SoundEffectsHelper.Instance.MakeEnemyShotSound();
                }
            }


            // Once spawned, the enemy will start shooting at the main character
            //  Goes through each child weapon script and start shooting those weapons
            foreach (WeaponScript laser in lasers)
            {
                if (laser != null && laser.enabled && laser.CanAttack)
                {
                    // True --> the bullets will hurt the player
                    // False --> the bullets will hurt the enemy
                    laser.Attack(true);
                    // Sound initiated
                    SoundEffectsHelper.Instance.MakeLaserSound();
                }
            }

            // Once the enemy moves but leaves the camera scene, then destroy the enemy object
            //   to save memory resources
            if (rendererComponent.IsVisibleFrom(Camera.main) == false)
            {
                Destroy(gameObject);
            }
        }
    }

    // Set the spawn as active for the enemy ships once they arrive at the camera
    private void Spawn()
    {
        hasSpawn = true;
        gunMoveScript.enabled = true;

        foreach (WeaponScript weapon in weapons)
        {
            weapon.enabled = true;
        }

        foreach (WeaponScript laser in lasers)
        {
            laser.enabled = true;
        }
    }

    void OnDestroy()
    {
        // Next Stage Load
        var levelClear = FindObjectOfType<StageClearScript>();
        levelClear.completeGame();
    }
}
