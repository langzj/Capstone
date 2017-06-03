

using UnityEngine;
using System.Collections;

//This class involves initiating the movement and shooting of the enemy characters
//  with the option to manipulate certain movement characteristics based on the public variables
public class BossScript : MonoBehaviour
{

    //The following private variables won't show in the Unity Engine UI
    //  this is how C# works in tandem with the unity engine
    private bool hasSpawn;
    private BossMoveScript moveScript;
    private WeaponScript[] weapons;
    private Collider2D coliderComponent;
    private SpriteRenderer rendererComponent;
    private ScrollingScript scrollingScript;
    private float timer;

    //Awake basically means that this function will run, no matter when the game started.
    //  We don't even need to call this function to have it activated
    void Awake()
    {
        // We use getComponentsInChildren because the sprite structure includes
        //  the weapon script assigned as a child script of the Enemy character.
        // This structure also anticipates certain characters that may have multiple weapons.
        weapons = GetComponentsInChildren<WeaponScript>();

        // The movement script is called
        moveScript = GetComponent<BossMoveScript>();

        scrollingScript = GetComponentInParent<ScrollingScript>();

        //This allows both collider and renderer to be invoked
        // which involes the physics engine componenet of Unity2D
        coliderComponent = GetComponent<Collider2D>();
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
        coliderComponent.enabled = false;

        moveScript.enabled = false;
        foreach (WeaponScript weapon in weapons)
        {
            weapon.enabled = false;
        }
    }

    void Update()
    {
        

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

            if (timer > 10)
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
        coliderComponent.enabled = true;
        moveScript.enabled = true;
        

        foreach (WeaponScript weapon in weapons)
        {
            weapon.enabled = true;
        }
    }

    void OnDestroy()
    {
        // Next Stage Load
        var levelClear = FindObjectOfType<StageClearScript>();
        levelClear.nextStage();
    }
}


