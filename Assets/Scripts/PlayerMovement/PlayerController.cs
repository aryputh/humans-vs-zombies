using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController playerController;
    public Rigidbody rb;
    public float speed = 1;
    public Camera cam;

    private void Start()
    {
        playerController = GetComponent<CharacterController>();
    }

    void FixedUpdate()
    {
        // player movement - forward, backward, left, right
        float horizontal = Input.GetAxis("Horizontal") * speed;
        float vertical = Input.GetAxis("Vertical") * speed;

        //playerController.Move((cam.transform.right * horizontal + cam.transform.forward * vertical) * Time.deltaTime);
        rb.AddForce((cam.transform.right * horizontal + cam.transform.forward * vertical) * Time.deltaTime);
    }
}
