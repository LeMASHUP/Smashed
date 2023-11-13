using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public InputManager inputManager;
    public Rigidbody rb;
    public float speed = 10;
    public float jumpForce = 200;
    public int dashSpeed = 10;
    public Vector3 move;
    private int indexJump = 0;
    [SerializeField] private string inputNameHorizontal;
    [SerializeField] private string inputNameVertical;

    private bool isGrounded;

    void Start()
    {
        inputManager.inputMaster.Movement.Jump.started += _ => Jump();
        //inputManager.inputMaster.Movement.Crouch.started += _ => Crouch();
        //inputManager.inputMaster.Movement.Crouch.canceled += _ => CancelCrouch();
    }

    private void Update()
    {
        float forward = inputManager.inputMaster.Movement.Forward.ReadValue<float>();
        move = transform.right * forward;
        move *= speed;
        rb.velocity = new Vector3(move.x, rb.velocity.y, move.z);

        if (forward > 0)
        {
            gameObject.transform.Find("Body").transform.rotation = Quaternion.LookRotation(Vector3.forward);
        }
        else if (forward < 0)
        {
            gameObject.transform.Find("Body").transform.rotation = Quaternion.LookRotation(Vector3.back);
        }
    }
 /*
    private void Crouch()
    {
        gameObject.GetComponent<Collider>().enabled = false;
    }

    private void CancelCrouch()
    {
       gameObject.GetComponent<Collider>().enabled = true;
    }

 */
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
