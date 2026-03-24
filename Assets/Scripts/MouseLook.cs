using System;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField]
    private float mouseSensitivity = 100f;
    private float xRotation = 0f;
    private float yRotation = 0f;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Vector3 initialRotation = transform.localRotation.eulerAngles;
        xRotation = initialRotation.x;
        yRotation = initialRotation.y;
    }
    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        yRotation += mouseX;
        xRotation -= mouseX;
        xRotation = Math.Clamp(xRotation, -90, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }  

}
