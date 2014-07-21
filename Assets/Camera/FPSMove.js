private var controller : CharacterController;
public var speed : float;

public var limitBack : float;

// Use this for initialization
function Awake () {
	controller = GetComponent(CharacterController);
}

// Update is called once per frame
function Update () {


	if (camera.enabled == false) return;
	// Get the input vector from keyboard or analog stick
	
	var vec = Vector3.zero;
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

	var directionVector = vec;
	
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
	
	var ret = transform.rotation * directionVector;
	ret *= speed;
	
	// Apply the direction to the CharacterMotor
	controller.Move(ret);
	
	
}

// Require a character controller to be attached to the same game object
@script RequireComponent (CharacterController)
@script RequireComponent (Camera)
@script AddComponentMenu ("Character/FPS Input Controller")
