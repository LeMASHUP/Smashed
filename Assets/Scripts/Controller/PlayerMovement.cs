using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    PlayerInput input;
    InputAction moveAction;
    InputAction jumpAction;
    [SerializeField] bool doubleJump;
    [SerializeField] float speed = 5;
    [SerializeField] float jumpForce = 5;
    [SerializeField] bool isGrounded;
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

    public void MovePlayer()
    {
        Vector2 direction = moveAction.ReadValue<Vector2>();
        transform.position += new Vector3(direction.x, 0, direction.y) * speed * Time.deltaTime;
        if (direction.x > 0)
        {
            body.transform.rotation = Quaternion.LookRotation(Vector3.forward);
        }
        else if (direction.x < 0)
        {
            body.transform.rotation = Quaternion.LookRotation(Vector3.back);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            isGrounded = true;
            doubleJump = false;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
