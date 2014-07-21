using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
[RequireComponent (typeof(Camera))]
[AddComponentMenu ("Character/FPS Input Controller")]
public class FPSMove : MonoBehaviour {
	private CharacterController controller;

	public float speed ;

	public float limitBack;

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


	}

	/// <summary>
	/// ラジコン風に動く.
	/// </summary>
	void MoveRadiCon(){

		Vector3 vec = Vector3.zero;
		if (Input.GetKey(KeyCode.A)){
			vec -= Vector3.right;
		}
		if (Input.GetKey(KeyCode.D)){
			vec += Vector3.right;
		}
		if (Input.GetKey(KeyCode.S)){
			vec -= Vector3.forward;
		}
		if (Input.GetKey(KeyCode.W)){
			vec += Vector3.forward;
		}
		
		Vector3 directionVector = vec;
		
		if (directionVector != Vector3.zero) {
			// Get the length of the directon vector and then normalize it
			// Dividing by the length is cheaper than normalizing when we already have the length anyway
			var directionLength = directionVector.magnitude;
			directionVector = directionVector / directionLength;
			
			// Make sure the length is no bigger than 1
			directionLength = Mathf.Min(1, directionLength);
			
			// Make the input vector more sensitive towards the extremes and less sensitive in the middle
			// This makes it easier to control slow speeds when using analog sticks
			directionLength = directionLength * directionLength;
			
			// Multiply the normalized direction vector by the modified length
			directionVector = directionVector * directionLength;
		}
		
		Vector3 ret = transform.rotation * directionVector;
		ret *= speed;
		
		// Apply the direction to the CharacterMotor
		controller.Move(ret);
	}

	
	/// <summary>
	/// 平行移動する.
	/// </summary>
	void MoveParallel(){

		Vector3 vec = Vector3.zero;

		if (Input.GetKey(KeyCode.A)){
			vec += transform.TransformDirection(Vector3.left);
		}
		if (Input.GetKey(KeyCode.D)){
			vec += transform.TransformDirection(Vector3.right);
		}
		if (Input.GetKey(KeyCode.S)){
			vec += transform.TransformDirection(Vector3.back);
		}
		if (Input.GetKey(KeyCode.W)){
			vec += transform.TransformDirection(Vector3.forward);
		}
		
		Vector3 directionVector = vec;
		
		if (directionVector != Vector3.zero) {
			// Get the length of the directon vector and then normalize it
			// Dividing by the length is cheaper than normalizing when we already have the length anyway
			var directionLength = directionVector.magnitude;
			directionVector = directionVector / directionLength;
			
			// Make sure the length is no bigger than 1
			directionLength = Mathf.Min(1, directionLength);
			
			// Make the input vector more sensitive towards the extremes and less sensitive in the middle
			// This makes it easier to control slow speeds when using analog sticks
			directionLength = directionLength * directionLength;
			
			// Multiply the normalized direction vector by the modified length
			directionVector = directionVector * directionLength;
		}
		
		Vector3 ret = new Vector3(directionVector.x, 0, directionVector.z);
		ret *= speed;
		
		// Apply the direction to the CharacterMotor
		controller.Move(ret);
	}

}

