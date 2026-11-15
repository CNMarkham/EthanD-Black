using Palmmedia.ReportGenerator.Core.Reporting.Builders;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed;
    public float slowspeed;
    public float fastspeed;


    public float mouseSens = 100f;
    
    
    public bool isCrouched;
    public bool isSprinting;

    float yaw;
    float pitch;

    Rigidbody rb;
    
    public GameObject ViewCamera;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // 1. Create a combined move direction from inputs
        Vector3 moveDirection = (transform.right * horizontal + transform.forward * vertical);

        // 2. Normalize so diagonal input doesn't exceed a magnitude of 1
        moveDirection = moveDirection.normalized;

        // 3. Apply force once
        rb.AddForce(moveDirection * speed);

        // 4. Clamp the horizontal velocity (X and Z) so it never exceeds your max speed (e.g., 5)
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        if (flatVel.magnitude > 5f)
        {
            Vector3 limitedVel = flatVel.normalized * 5f;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }

        float mouseX = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;

        pitch += mouseX * -1;
        yaw += mouseY;
        pitch = Mathf.Clamp(pitch, -20, 20);

        ViewCamera.transform.localEulerAngles = new Vector3(pitch, 0, 0);

        transform.localRotation = Quaternion.Euler(0, yaw, 0);

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            isCrouched = !isCrouched;
            
            if (isCrouched == true)
            {
                speed = slowspeed;
            }
           
            if (isCrouched == false)
            {
                speed = 25f;
            }

        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isSprinting = !isSprinting;

            if (isSprinting == true)
            {
                speed = fastspeed;
            }

            if (isSprinting == false)
            {
                speed = 25f;
            }
        }

    }
}
