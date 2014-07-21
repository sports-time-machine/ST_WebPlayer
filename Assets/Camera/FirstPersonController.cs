using UnityEngine;
using System.Collections;

namespace SportsTimeMachinePlayer.Common{

	public class FirstPersonController : MonoBehaviour {

		public float moveSpeed; 
		public float rotateSpeed;

		// Use this for initialization
		void Start () {
		}
		
		// Update is called once per frame
		void Update () {
			KeyEvent();
			MouseDragEvent();
		}

		private void KeyEvent(){

			float speed = 0.0f;
			Vector3 velocity = Vector3.zero;

			if (Input.GetKey(KeyCode.A)){
				velocity -= transform.right;
				speed = moveSpeed;
			}
			if (Input.GetKey(KeyCode.D)){
				velocity += transform.right;
				speed = moveSpeed;
			}
			if (Input.GetKey(KeyCode.S)){
				velocity -= transform.forward;
				speed = moveSpeed;
			}
			if (Input.GetKey(KeyCode.W)){
				velocity += transform.forward;
				speed = moveSpeed;
			}

			velocity.y = 0;
			velocity *= speed;

			transform.Translate(velocity);
			
		}

	
		private void MouseDragEvent(){
			if (Input.GetMouseButton(0)){
				transform.Rotate(Input.GetAxis("Mouse Y") * -1 * rotateSpeed, 
				                 Input.GetAxis("Mouse X") * rotateSpeed, 0);

			}

		}


	}

}