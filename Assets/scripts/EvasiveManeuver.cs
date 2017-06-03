using UnityEngine;
using System.Collections;


[System.Serializable]
public class Boundary
{
    public float xMin, xMax, yMin, yMax;
}

public class EvasiveManeuver : MonoBehaviour {

	public Vector2 startWait;			//time to wait before first maneuver these can be set in the editor, though they should be randomized
	public float dodge;					//
	public float smoothing;				//how quickly we transition through the dodge these should be random, not fixed
	public float tilt;					//how much it tilts in a given direction
	public Vector2 maneuverTime;		//how long in the maneuver
	public Vector2 maneuverWait;		//time to wait between maneuvers
	//public Boundary boundary;
	public float speed;
										//EnemyMover script is now deprecated, enemy speed is set in this script now
	private float currentSpeed;			//this is set in the mover script, this holds the "reading" we take of that set speed for use in this script
	private float targetManeuver;		//setting the cartesian direction toward which the ship will move
	private Rigidbody2D rb;				//our object container
	private float flag = 0;				//This flag value is used below to set when the ship stops moving forward 
	
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		rb.velocity = transform.forward * speed;
		currentSpeed = rb.velocity.x;
		StartCoroutine (Evade ());					//Using this method we can start/pause selected activites within the game
	}												//in this case when the enemy ship commits its next dodge maneuver
	
	IEnumerator Evade(){			//companion function specifically utilized by StartCoroutine, creates a thread by which we can affect object behavior...
		yield return new WaitForSeconds (Random.Range(startWait.x, startWait.y));		//...independent of the standard Update function, enables the use of yield 
		//rb.velocity = transform.forward * 0;											//...WaitForSeconds, this gives us control of the maneuvering in this context
		while (true){
			//Mathf.Sign is the sign of the value in the argument, appending negative sign inverts the convention, so negative and positive switch
			//in action this will return the opposite sign value relative to the given transform.position.axis, so being on the "+" side of the screen
			//will return a negative value.  The ship will then transform in moving in the direction opposite the closest edge.
			//here we ultimately set the cadence of the movement
			targetManeuver = Random.Range (1, dodge)* -Mathf.Sign (transform.position.y);

			yield return new WaitForSeconds(Random.Range(maneuverTime.x, maneuverTime.y));
			            
            //rb.velocity.z = ;
			//rb.velocity = transform.forward * speed;
			flag = Random.value;
			targetManeuver = 0;
			yield return new WaitForSeconds(Random.Range(maneuverWait.x, maneuverWait.y));
                     
            
            //flag = Random.Range(1.0f, 3.0f);		
			//rb.velocity.z = ;
		}	
	}
	
	
	// Update is called once per frame
	void FixedUpdate () {
		//here we are locking in the screen boundaries for the manuever

		//Debug.Log(flag);
		float newManeuver = Mathf.MoveTowards(rb.velocity.y, targetManeuver, Time.deltaTime * smoothing);
		if(flag < 0.5){
			currentSpeed = speed;
		}

		if(flag > 0.5 && rb.transform.position.x > 21){
			currentSpeed = 0;			
		}
		rb.velocity = new Vector3(currentSpeed, newManeuver, 0.0f);
		//defining the position constraints
		//rb.position = new Vector3(Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax), Mathf.Clamp(rb.position.y, boundary.yMin, boundary.yMax), 0.0f );
		//rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);				//setting the tilt limit
	}
	
	
	
}
