using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 6f;
    public float jumpSpeed = 8f;
    public float gravity = 20f;
    public Rigidbody rb;
    public int playerIndex;
    public bool jumping = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private Vector3 moveDirection = Vector3.zero;
    void Update()
    {
        CharacterController controller = GetComponent<CharacterController>();
        if (controller.isGrounded)
        {
            jumping = false;
        }

        moveDirection = new Vector3(Input.GetAxis("Horizontal" + playerIndex), 0, Input.GetAxis("Vertical" + playerIndex));
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= speed;

        if (!jumping)
        {
            jumping = true;
            moveDirection.y = jumpSpeed;
        }

        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);

    }
}
