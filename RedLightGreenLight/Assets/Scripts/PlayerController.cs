using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public int PlayerNumber;

	private float MoveSpeed = 50.0f;
	private float MaxSpeed = 100f;

	private float JumpSpeed = 300.0f;
	private bool Grounded = false;

	private Rigidbody2D Body;

	// Use this for initialization
	void Start () {
		Body = GetComponent<Rigidbody2D> ();
	}

	void Update() {
		Move ();
		Jump ();
	//	print (Body.velocity.magnitude);
	}

	void FixedUpdate () {

	}

	void Move() {
		float HorizontalMovement = Input.GetAxis ("Horizontal") * MoveSpeed * Time.deltaTime;
		Vector2 MoveForce = new Vector2 (HorizontalMovement, 0f);
		Vector2 GroundVelocity = new Vector2 (Body.velocity.x, 0f);
		if( GroundVelocity.magnitude < MaxSpeed )
			Body.AddForce (MoveForce);
	}

	void Jump() {
		if (Input.GetKeyDown (KeyCode.Space) && Grounded) {
			Grounded = false;
			Vector2 Force = transform.TransformDirection(Vector2.up) * JumpSpeed;
			Vector3 RelativeForce = transform.InverseTransformDirection(Force);
			Body.AddForce ( new Vector2(RelativeForce.x, RelativeForce.y) );
		}
	}

	void OnCollisionStay2D(Collision2D coll) {
		if (coll.gameObject.tag == "Floor") {
			Grounded = true;
		}
	}
}
