using Palmmedia.ReportGenerator.Core.Reporting.Builders;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 15f;
    public float mouseSens = 100f;

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

        //transform.localPosition += new Vector3(horizontal, 0, vertical) * Time.deltaTime * speed;
        //rb.velocity = new Vector3(horizontal * 100, -2, vertical * 100);
        rb.AddForce(transform.forward * vertical * 40);
        rb.AddForce(transform.right * horizontal * 40);

        float mouseX = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;

        //transform.Rotate(Vector3.up * mouseX);
        //transform.Rotate(Vector3.right * mouseY);

        pitch += mouseX * -1;
        yaw += mouseY;
        pitch = Mathf.Clamp(pitch, -20, 20);

        ViewCamera.transform.localEulerAngles = new Vector3(pitch, 0, 0);

        transform.localRotation = Quaternion.Euler(0, yaw, 0);

        if (Input.GetKey(KeyCode.Space))
        {

        }
    }
}
