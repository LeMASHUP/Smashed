using System.Collections;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    PlayerInput input;
    InputAction moveAction;
    InputAction jumpAction;
    [SerializeField] Vector2 direction;
    [SerializeField] bool doubleJump;
    [SerializeField] float speed = 5;
    [SerializeField] float jumpForce = 5;
    [SerializeField] bool isGrounded;
    [SerializeField] bool canMove = true;
    private Vector3 validDirection = Vector3.up;
    private float contactThreshold = 30;
    private float maxSpeed = 15;
    private GameObject body;

    void Start()
    {
        input = GetComponent<PlayerInput>();
        moveAction = input.actions.FindAction("Move");
        jumpAction = input.actions.FindAction("Jump");
        body = transform.Find("Body").gameObject;
    }

    void Update()
    {
        MovePlayer();
        JumpPlayer();
    }

    public void JumpPlayer()
    {
        if (jumpAction.triggered)
        {
            if (isGrounded || !doubleJump)
            {

                Rigidbody rb = transform.GetComponent<Rigidbody>();
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);

                if (!isGrounded)
                {
                    doubleJump = true;
                }
            }
        }
    }

    /*
    public void MovePlayer()
    {
        Vector2 direction = moveAction.ReadValue<Vector2>();

        Vector3 velocity = new Vector3(direction.x, 0, direction.y) * speed;

        transform.GetComponent<Rigidbody>().velocity = velocity;

        if (direction.x > 0)
        {
            body.transform.rotation = Quaternion.LookRotation(Vector3.forward);
        }
        else if (direction.x < 0)
        {
            body.transform.rotation = Quaternion.LookRotation(Vector3.back);
        }
    }
    */

    public void MovePlayer()
    {
        direction = moveAction.ReadValue<Vector2>(); 
      
        if (canMove)
        {
            Rigidbody rb = gameObject.GetComponent<Rigidbody>();
            rb.AddForce(direction * speed);

            Debug.Log(rb.velocity);
            if (direction.x > 0)
            {
                body.transform.rotation = Quaternion.LookRotation(Vector3.forward);
            }
            else if (direction.x < 0)
            {
                body.transform.rotation = Quaternion.LookRotation(Vector3.back);
            }
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Vector3 normal = collision.contacts[0].normal;
        Transform cube = collision.gameObject.transform;
        if (collision.transform.CompareTag("Ground"))
        {
            for (int i=0; i<collision.contacts.Length; i++)
            {
                if (Vector3.Angle((collision.contacts[i].normal), validDirection) <= contactThreshold)
                {
                    isGrounded = true;
                    canMove = true;
                    doubleJump = false;
                    break;
                }
                if (Vector3.Angle((collision.contacts[i].normal), Vector3.right) <= contactThreshold || Vector3.Angle((collision.contacts[i].normal), Vector3.left) <= contactThreshold)
                {
                    canMove = false;
                    break;
                }
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            isGrounded = false;
            canMove = true;
        }
    }
}
