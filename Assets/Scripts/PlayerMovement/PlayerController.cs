using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController playerController;
    public float speed;
    public Animator anim;
    public GameObject diePanel;
    public TMP_Text scoreText;

    [Tooltip("0 = idle, 1 = run, 2 = jump")]
    public int state;

    private bool isRunning;
    private bool isIdle;
    private bool isJumping;

    [HideInInspector]
    public int score;

    void Start()
    {
        isIdle = false;
        isRunning = false;
        isJumping = false;

        score = 0;
        scoreText.text = "Score: " + score;
    }

    void Update()
    {
        switch (state)
        {
            case 2:
                print("jump");

                isIdle = false;
                isRunning = false;
                isJumping = true;

                anim.SetBool("isJumping", true);
                anim.SetBool("isIdle", false);
                anim.SetBool("isRunning", false);

                state = 2;

                break;
            case 1:
                print("run");

                isIdle = false;
                isRunning = true;
                isJumping = false;

                anim.SetBool("isJumping", false);
                anim.SetBool("isIdle", false);
                anim.SetBool("isRunning", true);

                state = 1;

                break;
            case 0:
                print("idle");

                isIdle = true;
                isRunning = false;
                isJumping = false;

                anim.SetBool("isJumping", false);
                anim.SetBool("isIdle", true);
                anim.SetBool("isRunning", false);

                state = 0;

                break;
            default:
                print("error");

                isIdle = true;
                isRunning = false;
                isJumping = false;

                anim.SetBool("isJumping", false);
                anim.SetBool("isIdle", true);
                anim.SetBool("isRunning", false);

                state = 0;

                break;
        }
        
        //float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if(/*x != 0 || */z != 0)
        {
            state = 1;
            Vector3 move = /*(transform.right * x) + */(transform.forward * z);
            playerController.Move(move * speed * Time.deltaTime);
        }
        else if(/*x == 0 && */z == 0)
        {
            state = 0;
        }

        scoreText.text = "Score: " + score;
    }

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("Hole") || collision.gameObject.CompareTag("Zombie"))
		{
            print("You died!");
            diePanel.SetActive(true);

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
	}
}
