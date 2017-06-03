//The script was derived from the Unity2D Tutorial at http://pixelnest.io/tutorials/2d-game-unity/background-and-camera/

using UnityEngine;


public class SpecialEffectsHelper : MonoBehaviour {

    // Generate an instance of the Special Effects helper
    public static SpecialEffectsHelper Instance;

    //public ParticleSystem smokeEffect;
    public ParticleSystem fireEffect;

    //public ParticleSystem powerUpEffect
    public ParticleSystem powerUpEffect;

    //public ParticleSystem smokeEffect;
    public ParticleSystem bigEffect;

    void Awake()
    {
        // Register the singleton
        if (Instance != null)
        {
            Debug.LogError("Multiple instances of SpecialEffectsHelper!");
        }

        Instance = this;
    }

    // Activate the explosion
    public void Explosion(Vector3 position)
    {
        
        instantiate(fireEffect, position);
    }

    // Activate Power up
    public void PowerUp(Vector3 position)
    {

        instantiate(powerUpEffect, position);
    }

    // Activate the big explosion
    public void BigExplosion(Vector3 position)
    {

        instantiate(bigEffect, position);
    }

    // Create the explosions
    private ParticleSystem instantiate(ParticleSystem prefab, Vector3 position)
    {
        ParticleSystem newParticleSystem = Instantiate(
          prefab,
          position,
          Quaternion.identity
        ) as ParticleSystem;

        // Make sure it will be destroyed
        Destroy(
          newParticleSystem.gameObject,
          newParticleSystem.startLifetime
        );

        return newParticleSystem;
    }
}
