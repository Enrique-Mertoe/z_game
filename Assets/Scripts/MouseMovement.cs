using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    public float mouseSensitivity = 500f;
    private float xRotation = 0f;
    private float yRotation = 0f;
    public float topClamp = -50f;
    public float bottomClamp = 20f;

    void Start()
    {
        // Locking the cursor to the middle of the screen and making it invisible.
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //Mouse Inputs
        var  mouseX = Input.GetAxis("Mouse X") * mouseSensitivity/2 * Time.deltaTime;
        var mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

       //Mouse rotation
       
            xRotation -= mouseY;
        
        //clamp th rotation
        xRotation = Mathf.Clamp(xRotation, topClamp,bottomClamp);
        yRotation += mouseX;
        //APlying the rotation to the transform
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }
}
