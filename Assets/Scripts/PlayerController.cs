using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed = 6f;
    public float jumpSpeed = 8f;
    public float gravity = 9.81f;
    public Rigidbody rb;
    public int playerIndex;
    public bool jumping = false;
    public static bool oneWins = false;
    CharacterController controller;
    public static bool twoWins = false;
    public static bool dead = false;
    public GameObject winnerText;
    public Image image;
    public AudioClip jumpNoise;
    public AudioClip deathNoise;
    public AudioSource a;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();
        Time.timeScale = 0.3f;

    }
    float yJumpTarget;
    float yJumpSpeed;
    // Update is called once per frame
    private Vector3 moveDirection = Vector3.zero;
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
            Application.Quit();
        yJumpSpeed = Mathf.MoveTowards(yJumpTarget, yJumpSpeed, Time.deltaTime*3);
        if (controller.isGrounded)
        {
            jumping = false;
            a.PlayOneShot(jumpNoise);
        }

        moveDirection = new Vector3(Input.GetAxis("Horizontal" + playerIndex), 0, Input.GetAxis("Vertical" + playerIndex));
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= speed;

        if (!jumping)
        {
            jumping = true;
            yJumpTarget = 3;
        }
        yJumpTarget -= Time.deltaTime*15;
        moveDirection.y = yJumpSpeed;
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
        if (transform.position.y < -6) {
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
