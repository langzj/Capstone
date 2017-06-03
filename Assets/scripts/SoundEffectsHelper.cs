//The script was derived from the Unity2D Tutorial at http://pixelnest.io/tutorials/2d-game-unity/background-and-camera/

using UnityEngine;
using System.Collections;


public class SoundEffectsHelper : MonoBehaviour
{

    //Initiate the sounds effects instance
    public static SoundEffectsHelper Instance;

    // This allows the user to set the sounds effects
    public AudioClip explosionSound;
    public AudioClip playerShotSound;
    public AudioClip enemyShotSound;
    public AudioClip enemyShotSound2;
    public AudioClip monsterDeath;
    public AudioClip laserSound;
    public AudioClip healthUpgradeSound;
    public AudioClip speedUpgradeSound;

    void Awake()
    {
        // Make sure only one script is initiation, not multiple
        if (Instance != null)
        {
            Debug.LogError("Multiple instances of SoundEffectsHelper!");
        }
        Instance = this;
    }

    // Call function to make explosion sound
    public void MakeExplosionSound()
    {
        MakeSound(explosionSound);
    }

    // Call function to make playershot sound
    public void MakePlayerShotSound()
    {
        MakeSound(playerShotSound);
    }

    // Call function to make enemyshot sound
    public void MakeEnemyShotSound()
    {
        MakeSound(enemyShotSound);
    }

    // Call function to make enemyshot sound 2
    public void MakeEnemyShotSound2()
    {
        MakeSound(enemyShotSound2);
    }

    // Call function to make laser
    public void MakeLaserSound()
    {
        MakeSound(laserSound);
    }

    // Call function to make monster death
    public void MakeMonsterDeathSound()
    {
        MakeSound(monsterDeath);
    }

    // Call function to make monster death
    public void MakeHealthUpgradeSound()
    {
        MakeSound(healthUpgradeSound);
    }

    // Call function to make monster death
    public void MakeSpeedUpgradeSound()
    {
        MakeSound(speedUpgradeSound);
    }

    // Function description for the make sound portion
    private void MakeSound(AudioClip originalClip)
    {
        //Play the sound that was passed in with the sound
        AudioSource.PlayClipAtPoint(originalClip, transform.position);
    }
}