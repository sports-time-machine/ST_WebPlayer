using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
[RequireComponent (typeof(Camera))]
[AddComponentMenu ("Character/FPS Input Controller")]
public class FPSMove : MonoBehaviour {
	private CharacterController controller;

	public float arrowSpeed = 0.25f; 

	public float wheelSpeed = 0.50f;

	// Use this for initialization
	void Awake () {
		controller = GetComponent<CharacterController>();
	}

	// Update is called once per frame
	void Update () {
		// カメラが使用されていないなら移動を行わない.
		if (camera.enabled == false) return;

		// 左シフトキー押しで平行移動.
		if (Input.GetKey (KeyCode.LeftShift)) MoveParallel();
		// 左シフトキーを離すとラジコン移動.
		else MoveRadiCon();

		// 中クリック動作.
		// 水平移動.
		if (Input.GetMouseButton(2)){
			
			Vector3 vec = Vector3.zero;
			float speed = 0.0f;

			float moveX = Input.GetAxis("Mouse X");
			float moveY = Input.GetAxis("Mouse Y");

			if (moveX > 0) vec += Vector3.left;
			if (moveX < 0) vec += Vector3.right;
			if (moveY > 0) vec += Vector3.down;
			if (moveY < 0) vec += Vector3.up;

			speed = arrowSpeed * new Vector2(moveX, moveY).magnitude;

			vec = Normalize(vec);
			Vector3 ret = transform.rotation * vec;
			ret *= speed;
			// Apply the direction to the CharacterMotor
			controller.Move(ret);
		}
	}

	/// <summary>
	/// ラジコン風に動く.
	/// </summary>
	void MoveRadiCon(){

		float speed = 0.0f;

		Vector3 vec = Vector3.zero;
		if (Input.GetKey(KeyCode.A)){
			vec -= Vector3.right;
			speed = arrowSpeed;
		}
		if (Input.GetKey(KeyCode.D)){
			vec += Vector3.right;
			speed = arrowSpeed;
		}
		if (Input.GetKey(KeyCode.S) || (Input.GetAxisRaw("Mouse ScrollWheel") < 0)){
			if (Input.GetKey(KeyCode.S)) speed = arrowSpeed;
			else speed = wheelSpeed;
			vec -= Vector3.forward;
		}
		if (Input.GetKey(KeyCode.W) || (Input.GetAxisRaw("Mouse ScrollWheel") > 0)){
			if (Input.GetKey(KeyCode.W)) speed = arrowSpeed;
			else speed = wheelSpeed;
			vec += Vector3.forward;
		}
		
		vec = Normalize(vec);
		Vector3 ret = transform.rotation * vec;
		ret *= speed;
		
		// Apply the direction to the CharacterMotor
		controller.Move(ret);
	}

	
	/// <summary>
	/// 平行移動する.
	/// </summary>
	void MoveParallel(){

		float speed = 0.0f;

		Vector3 vec = Vector3.zero;

		if (Input.GetKey(KeyCode.A)){
			vec += transform.TransformDirection(Vector3.left);
			speed = arrowSpeed;
		}
		if (Input.GetKey(KeyCode.D)){
			vec += transform.TransformDirection(Vector3.right);
			speed = arrowSpeed;
		}
		if (Input.GetKey(KeyCode.S) || (Input.GetAxisRaw("Mouse ScrollWheel") < 0)){
			vec += transform.TransformDirection(Vector3.back);
			if (Input.GetKey(KeyCode.S)) speed = arrowSpeed;
			else speed = wheelSpeed;
		}
		if (Input.GetKey(KeyCode.W) || (Input.GetAxisRaw("Mouse ScrollWheel") > 0)){
			vec += transform.TransformDirection(Vector3.forward);
			if (Input.GetKey(KeyCode.W)) speed = arrowSpeed;
			else speed = wheelSpeed;
		}

		vec = Normalize(vec);
		Vector3 ret = new Vector3(vec.x, 0, vec.z);
		ret *= speed;
		
		// Apply the direction to the CharacterMotor
		controller.Move(ret);
	}

	private Vector3 Normalize(Vector3 vec){
		
		if (vec != Vector3.zero) {
			// Get the length of the directon vector and then normalize it
			// Dividing by the length is cheaper than normalizing when we already have the length anyway
			float directionLength = vec.magnitude;
			vec = vec / directionLength;
			
			// Make sure the length is no bigger than 1
			directionLength = Mathf.Min(1, directionLength);
			
			// Make the input vector more sensitive towards the extremes and less sensitive in the middle
			// This makes it easier to control slow speeds when using analog sticks
			directionLength = directionLength * directionLength;
			
			// Multiply the normalized direction vector by the modified length
			vec = vec * directionLength;
		}

		return vec;
	}

}

