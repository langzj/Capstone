//The script was derived from the Unity2D Tutorial at http://pixelnest.io/tutorials/2d-game-unity/background-and-camera/

using UnityEngine;


public class ShotScript : MonoBehaviour {

    // Set default of the shot damage at 1
    public int damage = 1;

    // Another boolean variable to assess whether emeny is shot
    public bool isEnemyShot = false;

    void Start()
    {
        // Make sure all bullets are destroyed after a certain time so that the bullet does not overload the memory
        Destroy(gameObject, 10); 
    }
}
