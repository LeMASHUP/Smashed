using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    PlayerInput input;
    InputAction moveAction;
    InputAction jumpAction;
    [SerializeField] float speed = 5;
    [SerializeField] float jumpForce = 5;
    private GameObject body;
    // Start is called before the first frame update
    void Start()
    {
        PlayerInput input = GetComponent<PlayerInput>();
        moveAction = input.actions.FindAction("Move");
        jumpAction = input.actions.FindAction("Jump");
        body = transform.Find("Body").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }

    public void JumpPlayer()
    {
        Rigidbody rb = transform.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.AddForce(Vector3.up * jumpForce);
    }

    public void MovePlayer()
    {
        Vector2 direction = moveAction.ReadValue<Vector2>();
        transform.position += new Vector3(direction.x, 0, direction.y) * speed *Time.deltaTime;
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
