using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public InputManager inputManager;
    public Rigidbody rb;
    public float speed = 10;
    public float jumpForce = 200;

    private bool isGrounded;

    void Start()
    {
        inputManager.inputMaster.Movement.Jump.started += _ => Jump();
    }

    private void Jump()
    {
        if (isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce);
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
        }
    }
}
