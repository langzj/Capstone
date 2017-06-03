using UnityEngine;
using System.Collections;

public class PowerUpScript : MonoBehaviour {

    public int addHealth;
    public float increaseSpeed;

	
    public int getHealth() {
        return addHealth;
    }

    public float getSpeed()
    {
        return increaseSpeed;
    }

    public void vanish()
    {
        SpecialEffectsHelper.Instance.PowerUp(transform.position);
        Destroy(gameObject);
    }
}
