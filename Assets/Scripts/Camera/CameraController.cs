using UnityEngine;


public class CameraController : MonoBehaviour
{
    Camera mainCamera;
    private GameObject player1;
    private GameObject player2;

    private void Awake()
    {
        mainCamera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player1 == null)
        {
            player1 = GameObject.Find("Player1(Clone)");
        }
        if (player2 == null)
        {
            player2 = GameObject.Find("Player2(Clone)");
        }
        if (player1 != null && player2 != null)
        {
            float dist = Vector3.Distance(player1.transform.position, player2.transform.position);
            float dist2 = Vector3.Distance(player2.transform.position, player1.transform.position);
            if (dist > 35)
            {
                mainCamera.orthographicSize = 20;
            }
        }
    }
}
