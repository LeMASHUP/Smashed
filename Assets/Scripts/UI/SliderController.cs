using UnityEngine;
using Unity.UI;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class SliderController : MonoBehaviour
{
    Slider slider;
    private GameObject player;
    public string playerName;
    void Start()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            player = GameObject.Find(playerName);
        }

        if (player != null)
        {
            slider.value = player.GetComponent<LifePointManager>().lifePoint;
        }
    }
}
