using UnityEngine;
using TMPro;

public class TextController : MonoBehaviour
{
    private TMP_Text text;
    private GameObject player;
    public string playerName;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TMP_Text>();
    }

    
    void Update()
    {
        if (player == null)
        {
            player = GameObject.Find(playerName);
        }

        if (player != null)
        {
            text.enabled = true;
            Vector3 pos = new Vector3(player.transform.position.x + 2.2f, player.transform.position.y + 3, 0);
            text.transform.position = pos;
        }
    }
}
