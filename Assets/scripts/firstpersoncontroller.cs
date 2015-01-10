using UnityEngine;
using System.Collections;

public class firstpersoncontroller : MonoBehaviour {

	public float movementSpeed = 10.0f;
	public float mouseSensitivity = 2.0f;
	public float jumpSpeed = 5.0f;

	float verticalRotation = 0;
	public float upDownRange = 60.0f;

	float verticalVelocity = 0;


	// Use this for initialization
	void Start () {
		Screen.lockCursor = true;
	}
	
	// Update is called once per frame
	void Update () {
		CharacterController cc = GetComponent<CharacterController> ();


		//rotation

		float rotleftright = Input.GetAxis ("Mouse X") * mouseSensitivity;
		transform.Rotate (0, rotleftright, 0);

		verticalRotation -= Input.GetAxis ("Mouse Y") * mouseSensitivity;
		verticalRotation = Mathf.Clamp (verticalRotation, -upDownRange, upDownRange);
		Camera.main.transform.localRotation = Quaternion.Euler (verticalRotation, 0, 0);

		//movement

		float forwardSpeed = Input.GetAxis ("Vertical") * movementSpeed;
		float sideSpeed = Input.GetAxis ("Horizontal")* movementSpeed;

		verticalVelocity += Physics.gravity.y * Time.deltaTime;



		if (cc.isGrounded && Input.GetButtonDown ("Jump")) {
			verticalVelocity = jumpSpeed;
							}

		if (!cc.isGrounded) {
			forwardSpeed = forwardSpeed / 2;
			sideSpeed = sideSpeed / 2;
				}

		Vector3 speed = new Vector3 (sideSpeed, verticalVelocity, forwardSpeed);

		speed = transform.rotation * speed;

		cc.Move (speed * Time.deltaTime);

	}
}
