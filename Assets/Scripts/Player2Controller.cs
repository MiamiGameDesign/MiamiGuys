using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player2Controller : MonoBehaviour
{
    public float speed = 6f;
    public float jumpSpeed = 8f;
    public float gravity = 9.81f;
    public Rigidbody rb;
    public int playerIndex;
    public bool jumping = false;
    CharacterController controller;
    public static bool dead = false;
    public GameObject winnerText;
    public Image image;
    public AudioClip jumpNoise;
    public AudioClip deathNoise;
    public AudioSource a;

    private bool jumpButtonPressed = false; // New variable to track jump button press

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();
    }

    float yJumpTarget;
    float yJumpSpeed;

    // Update is called once per frame
    private Vector3 moveDirection = Vector3.zero;

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
            Application.Quit();
        if (Input.GetKey(KeyCode.R))
            SceneManager.LoadScene("MainMenu");

        yJumpSpeed = Mathf.MoveTowards(yJumpTarget, yJumpSpeed, Time.deltaTime * 3);

        if (controller.isGrounded)
        {
            jumping = false;
            
        }

        moveDirection = new Vector3(Input.GetAxis("Horizontal" + playerIndex), 0, Input.GetAxis("Vertical" + playerIndex));
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= speed;

        // Check if the correct jump button is pressed based on player index
        if (playerIndex == 2 && Input.GetKeyDown(KeyCode.LeftControl))
        {
            jumpButtonPressed = true;
        }
        else if (playerIndex == 1 && Input.GetKeyDown(KeyCode.RightControl))
        {
            jumpButtonPressed = true; 
        }
        else
        {
            jumpButtonPressed = false;
        }

        // Handle jumping only when the jump button is pressed
        if (!jumping && jumpButtonPressed)
        {
            a.PlayOneShot(jumpNoise);
            jumping = true;
            yJumpTarget = 5;
        }

        yJumpTarget -= Time.deltaTime * 10;
        moveDirection.y = yJumpSpeed;
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);

        if (transform.position.y < -6)
        {
            if (!dead)
            {
                a.PlayOneShot(deathNoise);
                dead = true;
            }
            Time.timeScale = 0;
            image.enabled = true;
            winnerText.SetActive(true);
        }
    }
}
