using UnityEngine;
using System.Collections;



public class BossHealthScript : MonoBehaviour {

    //This variable sets the hp for the ships. The default is 1, but it can be adjusted.
    public int hp = 1;

    // This variable makes sure whether the health script is for the enemy or the player
    public bool isEnemy = true;
    

    //Function that is invoked when the character is hit with the bullet
    //  damageCount variable refers to the power of the bullet
    public void Damage(int damageCount)
    {
        // Reduce damage by the damage count
        hp -= damageCount;

        // If the ship is hit by the bullet and has hp equal or lower than zero,
        //   then the ship is destroyed
        if (hp <= 0)
        {
            // The explosion occurs as its sound
            SpecialEffectsHelper.Instance.BigExplosion(transform.position);
            SoundEffectsHelper.Instance.MakeExplosionSound();

            ScoreScript.Instance.AddScore(1000);

            Destroy(gameObject);           

        }
    }

    
    // This is triggered by using the box collider engine of the Unity2D engine.
    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        // Invoke the shot script, which indicates whether the character is hit with the shot
        ShotScript shot = otherCollider.gameObject.GetComponent<ShotScript>();
        if (shot != null)
        {
            // The function will be called only when the correct character is hit with the bullet
            //  You don't want enemies dying by friendly fire
            if (shot.isEnemyShot != isEnemy)
            {
                // Call the damage function as provided above
                Damage(shot.damage);

                // Destroy the shot object in the game
                Destroy(shot.gameObject);
            }
        }
    }
}
