using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player1Controller : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 1f;
    public float groundCheckDistance = 1.25f;
    private Rigidbody rb;
    private bool isGrounded;
    public int playerIndex;
    public bool jumping = false;
    public static bool dead = false;
    public Image image;
    public AudioClip jumpNoise;
    public AudioClip deathNoise;
    public AudioSource a;
    public GameObject survivalText;
    private float startTime, timeElapsed;

    void Start()
    {
        startTime = Time.time;
        rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
    }
    private void FixedUpdate()
    {
        // Move the player horizontally and vertically based on input
        float moveHorizontal = Input.GetAxis("Horizontal" + playerIndex);
        float moveVertical = Input.GetAxis("Vertical" + playerIndex);

        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical);
        movement.Normalize();
        movement *= moveSpeed * Time.deltaTime;
        Vector3 newPosition = rb.position + transform.TransformDirection(movement);
        rb.MovePosition(newPosition);
        if (Input.GetButtonDown("Jump") && isGrounded)
        {   
            a.PlayOneShot(jumpNoise);
            Vector3 vel = rb.velocity;
            vel.y = jumpForce * 5;
            rb.velocity = vel;
        }
    }
    // Update is called once per frame
    void Update()
    {
        //time scale and game quitting keys
        if (Input.GetKey(KeyCode.Escape))
            Application.Quit();
        if (Input.GetKey(KeyCode.R))
            SceneManager.LoadScene("MainMenu");
        if (Time.timeScale != 0)
            timeElapsed = (Time.time - startTime) * Time.timeScale;
        //Debug.Log("Time elapsed: " + timeElapsed.ToString());

        //check if player dies
        if (transform.position.y < -6)
        {
            if (!dead)
            {
                a.PlayOneShot(deathNoise);
                dead = true;
            }
            Time.timeScale = 0;
            image.enabled = true;
            survivalText.SetActive(true);
            survivalText.GetComponent<Text>().text = "You survived for " + (Mathf.Round(timeElapsed * 100f) / 100f) + " seconds!!!! :D";
        }
    }
    void OnCollisionStay(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            if (Vector3.Dot(contact.normal, Vector3.up) > 0.1)
            {
                isGrounded = true;
                return;
            }
        }

        isGrounded = false;
    }

    void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }

}
