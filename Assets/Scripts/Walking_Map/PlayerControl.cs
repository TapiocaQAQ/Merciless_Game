using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

	public Camera playerCamera;
	public float mouseSensitivity = 100f;
	float xRotation = 0f;
	float yRotation = 0f;

	public CharacterController playerController;
	public float moveSpeed = 2f;

	public Vector3 fallSpeed;
	float distToGround;
	bool isJumped = false;
	bool inAir = false;
	public float airTime = 0.0f;

	bool IsGrounded(float margin = 0.1f){
		return Physics.Raycast(transform.position, -Vector3.up, distToGround + margin);
	}

	// Use this for initialization
	void Start () {
		Cursor.lockState = CursorLockMode.Locked;
		distToGround = GetComponent<Collider>().bounds.extents.y;
		transform.position = new Vector3(Config.playerSpawnX, transform.position.y, Config.playerSpawnZ);
	}
	
	// Update is called once per frame
	void Update () {
		// control the camera with mouse
		float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
		float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

		xRotation -= mouseY;
		xRotation = Mathf.Clamp(xRotation, -90f, 90f);
		yRotation += mouseX;
		if(yRotation > 360 || yRotation < 0) yRotation %= 360;

		playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
		// rotate body with mouse
		transform.localRotation = Quaternion.Euler(0f, yRotation, 0f);

		// control the jump
		if(isJumped){
			airTime += Time.deltaTime;
			if(airTime>0.6 && IsGrounded(0.3f)){
				isJumped = false;
				inAir = false;
				airTime = 0.0f;
				playerCamera.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
			}else{
				if(airTime<=0.2){
					playerCamera.transform.localPosition -= new Vector3(0.0f, 1f, 0.0f) * Time.deltaTime;
				}else if(airTime<=0.4){
					if(!inAir){
						fallSpeed.y = 3;
						inAir = true;
					}
					playerCamera.transform.localPosition += new Vector3(0.0f, 1f, 0.0f) * Time.deltaTime;
				}
			}
		}else if(Input.GetAxis("Jump")>0 && IsGrounded(0.3f)){
			isJumped = true;
			airTime = 0.0f;
		}

		// control the movement
		float moveH = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
		float moveV = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
		if(isJumped && airTime<=0.1) {moveH/=2; moveV/=2;}
		playerController.Move(transform.right*moveH + transform.forward*moveV);

		// control the falling
		fallSpeed.y += Config.gravity * Time.deltaTime;
		playerController.Move(fallSpeed * Time.deltaTime);
	}
}
