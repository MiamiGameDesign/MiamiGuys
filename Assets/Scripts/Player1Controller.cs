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
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            a.PlayOneShot(jumpNoise);
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
            survivalText.GetComponent<Text>().text = "You survived for " + (Mathf.Round(timeElapsed * 100f) / 100f) % 60 + " seconds!!!! :D";
        }
        //movement
        RaycastHit hitInfo;
        
        if (Physics.Raycast(transform.position, Vector3.down, out hitInfo, groundCheckDistance))
        {
            
            isGrounded = true;
        }
            
        else
        {
            //Debug.Log("grounded: " + isGrounded + " hit name: " +  hitInfo.collider.name);
            isGrounded = false;
        }
        Vector3 jumpVelocity = Vector3.up * jumpForce / rb.mass;
        float timeToApex = jumpVelocity.y / -Physics.gravity.y;
        float distanceToLand = jumpVelocity.x * timeToApex;

        // Draw a visible raycast to indicate where the player will land if they jump
        Vector3 raycastOrigin = transform.position + Vector3.up * 0.1f;
        Vector3 raycastDirection = jumpVelocity.normalized;
        Debug.DrawRay(raycastOrigin, raycastDirection * distanceToLand, Color.green);

    }
    
}
