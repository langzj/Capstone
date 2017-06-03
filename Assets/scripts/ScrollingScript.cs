//The script was derived from the Unity2D Tutorial at http://pixelnest.io/tutorials/2d-game-unity/background-and-camera/

using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class ScrollingScript : MonoBehaviour {

    //Set the default speed of the scroll script into the public variable
    public Vector2 speed = new Vector2(10, 10);
    
    //Set the default deirection of the scroll script, that moves from right to left
    public Vector2 direction = new Vector2(-1, 0);

    //This variable to check whether the particular character is linked to the camera
    //   In this game, the player ship is going to be linked to the camera, otherwise it'll be default
    public bool isLinkedToCamera = false;

    // This makes sure the background scene is looping 
    public bool isLooping = false;

    
    private List<SpriteRenderer> backgroundPart;

    // This function is called for parallax scrolling, that is, the background is looping as soon as the camera
    //   approaches the end of the scene.
    // We are struggling to understand this code fully, but here is the source: http://pixelnest.io/tutorials/2d-game-unity/parallax-scrolling/
    //   We will keep trying to add comments to understand how this works.
    void Start()
    {
       
        if (isLooping)
        {
            
            backgroundPart = new List<SpriteRenderer>();

            for (int i = 0; i < transform.childCount; i++)
            {
                Transform child = transform.GetChild(i);
                SpriteRenderer r = child.GetComponent<SpriteRenderer>();

                
                if (r != null)
                {
                    backgroundPart.Add(r);
                }
            }

            
            backgroundPart = backgroundPart.OrderBy(
              t => t.transform.position.x
            ).ToList();
        }
    }

    void Update()
    {
        // Movement
        Vector3 movement = new Vector3(
          speed.x * direction.x,
          speed.y * direction.y,
          0);

        movement *= Time.deltaTime;
        transform.Translate(movement);

        // Move the camera
        if (isLinkedToCamera)
        {
            Camera.main.transform.Translate(movement);
        }

        if (isLooping)
        {
            // Get the first object.
            // The list is ordered from left (x position) to right.
            SpriteRenderer firstChild = backgroundPart.FirstOrDefault();

            if (firstChild != null)
            {
                // Check if the child is already (partly) before the camera.
                // We test the position first because the IsVisibleFrom
                // method is a bit heavier to execute.
                if (firstChild.transform.position.x < Camera.main.transform.position.x)
                {
                    // If the child is already on the left of the camera,
                    // we test if it's completely outside and needs to be
                    // recycled.
                    if (firstChild.IsVisibleFrom(Camera.main) == false)
                    {
                        // Get the last child position.
                        SpriteRenderer lastChild = backgroundPart.LastOrDefault();

                        Vector3 lastPosition = lastChild.transform.position;
                        Vector3 lastSize = (lastChild.bounds.max - lastChild.bounds.min);

                        // Set the position of the recyled one to be AFTER
                        // the last child.
                        // Note: Only work for horizontal scrolling currently.
                        firstChild.transform.position = new Vector3(lastPosition.x + lastSize.x, firstChild.transform.position.y, firstChild.transform.position.z);

                        // Set the recycled child to the last position
                        // of the backgroundPart list.
                        backgroundPart.Remove(firstChild);
                        backgroundPart.Add(firstChild);
                    }
                }
            }
        }
    }
}
