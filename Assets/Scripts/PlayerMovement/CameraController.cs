using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerTransform;
    public float maxCamAngle = 90f;

    private float xRot = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float x = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float y = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRot -= y;
        xRot = Mathf.Clamp(xRot, 40, 40); //xRot = Mathf.Clamp(xRot, -maxCamAngle, maxCamAngle);

        transform.localRotation = Quaternion.Euler(xRot, 0, 0);
        playerTransform.Rotate(Vector3.up * x);
    }
}
