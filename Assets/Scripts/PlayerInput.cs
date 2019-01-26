using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour {
    public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
    public RotationAxes axes = RotationAxes.MouseX;
    public float sensitivityX = 15F;
    public float sensitivityY = 15F;
    public float minimumX = -360F;
    public float maximumX = 360F;
    public float minimumY = -60F;
    public float maximumY = 60F;
    float rotationY = 0F;

    public enum direction { forward, left, right, backward }

    public float speed = 6.0F;
    public float rotSpeed = 90; // rotate speed in degrees/second

    private Vector3 moveDirection = Vector3.zero;
    public Transform playerTransform;

    public enum inputTypes
    {
        keyboard,
        controller
    };

    public inputTypes currentInput = inputTypes.controller;

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //handles player movement
    public void handlePlayerMovement()
    {
        switch (currentInput)
        {
            case inputTypes.keyboard:
                handleKeyboardInput();
                break;

            case inputTypes.controller:
                handleControllerInput();
                break;
        }

    }

    public void handleKeyboardInput()
    {
        //-----------MOUSE--------------
        if (axes == RotationAxes.MouseXAndY)
        {
            float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;

            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
            rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

            transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
        }
        else if (axes == RotationAxes.MouseX)
        {
            transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);
        }
        else
        {
            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
            rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

            transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
        }
        

        //----------KEYBOARD-------------
        Vector3 pos = playerTransform.position;

        if (Input.GetKey("w"))
        {
            pos = applyMovementInDirection(direction.forward, pos);
        }
        if (Input.GetKey("s"))
        {
            pos = applyMovementInDirection(direction.backward, pos);
        }
        if (Input.GetKey("d"))
        {
            pos = applyMovementInDirection(direction.right, pos);
        }
        if (Input.GetKey("a"))
        {
            pos = applyMovementInDirection(direction.left, pos);
        }


        playerTransform.position = pos;
    }

    //applies a movement based on the mouse look direction, modified by the direction passed. (to allow strafing with 'wasd')
    public Vector3 applyMovementInDirection(direction dir, Vector3 pos) {
        float angleModifier=0;
        switch (dir) {
            case direction.forward:
                angleModifier = 0;
                break;
            case direction.left:
                angleModifier = -90;
                break;
            case direction.right:
                angleModifier = 90;
                break;
            case direction.backward:
                angleModifier = 180;
                break;          
        }

        float scalar = speed / 10 * Time.deltaTime;
        float angle = Mathf.Deg2Rad * (transform.eulerAngles.y+angleModifier);
        pos.z += scalar * Mathf.Cos(angle);
        pos.x += scalar * Mathf.Sin(angle);

        return pos;
    }

    public void handleControllerInput()
    {
        CharacterController controller = GetComponent<CharacterController>();
        if (controller.isGrounded || 1 == 1)
        {
            transform.Rotate(0, Input.GetAxis("Horizontal") * rotSpeed * Time.deltaTime, 0);
            moveDirection = new Vector3(0, 0, Input.GetAxis("Vertical"));
            //Camera.main.transform.Rotate(Input.GetAxis("ControllerRightStickVert"), 0, 0);
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            //if (Input.GetButton("Jump"))
            // moveDirection.y = jumpSpeed;

            //GetComponent<AudioSource>().PlayOneShot(walk);

        }
        //moveDirection.y -= gravity * Time.deltaTime * collisions;
        controller.Move(moveDirection * Time.deltaTime);
    }
}
