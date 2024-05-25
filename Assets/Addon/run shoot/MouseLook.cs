using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
	public Transform playerBody;
	
	public float mouseSensitivity = 100f;
	
	float xRotation = 0f;
	
	
	public Transform targetObject; 
	public float rotationSpeed = 5f;
	public float leftmaxf;
	public float rightmax;
	
	public float minAngle = -80f;
	public float maxAngle = 80f;

	private float currentAngle = 0f;

    // Start is called before the first frame update
    void Start()
    {
	    Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
	{  
		
		if (Input.GetKey(KeyCode.E))
		{
			
			
			// Rotate the camera 90 degrees to the right when "E" is pressed
			//RotateTargetObject(90f);
			//Debug.Log("AYLANSIN");
		}
		
		
	    float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
	    float mouseY = Input.GetAxis("Mouse Y") *mouseSensitivity * Time.deltaTime;
	   
	    xRotation -=mouseY;
	    xRotation = Mathf.Clamp(xRotation, -90f, 90f);
	     transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
	    playerBody.Rotate(Vector3.up * mouseX);
	    
		lefthandcheck chap=FindObjectOfType<lefthandcheck>();
		righthandcheck ong=FindObjectOfType<righthandcheck>();
		PlayerMovment move=FindObjectOfType<PlayerMovment>();
		
		
		if (chap.chap==true&&move.wallrun==true)
	    {
		    
			RotateCamera(-1); 	
			// transform.localRotation = Quaternion.Euler(0, 0,-20);
		    	
		    
		    
		    
		   
	    }
		if(ong.ong==true&&move.wallrun==true){
	    	
			RotateCamera(1);
	    	  
			//transform.localRotation = Quaternion.Euler(0, 0, 20f);
		   
		}
	    
	
    
	}
	
	void RotateCamera(float direction)
	{
		float angleToRotate = direction * rotationSpeed * Time.deltaTime;
		currentAngle += angleToRotate;

		// Limit the rotation angle between minAngle and maxAngle
		currentAngle = Mathf.Clamp(currentAngle, minAngle, maxAngle);

		transform.localRotation = Quaternion.Euler(0f,0f,currentAngle);
		
	}
}
