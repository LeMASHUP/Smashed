using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public InputManager inputManager;
    public Rigidbody rb;
    public float speed = 10;
    public float jumpForce = 200;
    private int indexJump = 0;

    private bool isGrounded;

    void Start()
    {
        inputManager.inputMaster.Movement.Jump.started += _ => Jump();
    }

    private void Update()
    {
        float forward = inputManager.inputMaster.Movement.Forward.ReadValue<float>();
        Vector3 move = transform.right * forward;
        move *= speed;
        rb.velocity = new Vector3(move.x, rb.velocity.y, move.z);
    }

    private void Jump()
    {
        if (isGrounded || indexJump == 0)
        {
            rb.velocity = Vector3.zero;
            rb.AddForce(Vector3.up * jumpForce);
            if (indexJump == 0)
            {
                indexJump = 1;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            isGrounded = false;
            if (indexJump == 1)
            {
                indexJump = 0;
            }
        }
    }
}
