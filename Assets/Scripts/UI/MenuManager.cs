using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static bool p1IsDead = false;
    public static bool p2IsDead = false;
    public static MenuManager instance;
    [SerializeField] private Image p1Sprite;
    [SerializeField] private Image p2Sprite;

    public void Start()
    {
        p1IsDead = LifePointManager.p1IsDead;
        p2IsDead = LifePointManager.p2IsDead;
        if (p1Sprite != null && p2Sprite != null)
        {
            if (p1IsDead == true)
            {
                p1Sprite.transform.position = new Vector3(175f, 172.29f, 0f);
                p1Sprite.rectTransform.sizeDelta = new Vector2(300, 300);

                p2Sprite.transform.position = new Vector3(991.3214f, 572.9106f, 0f);
                p2Sprite.rectTransform.sizeDelta = new Vector2(403.4643f, 667.6254f);
            }
            else if (p2IsDead == true)
            {
                p1Sprite.transform.position = new Vector3(1036.586f, 544.9369f, 0f);
                p1Sprite.rectTransform.sizeDelta = new Vector2(653.1727f, 668.8226f);

                p2Sprite.transform.position = new Vector3(168.23f, 173.26f, 0f);
                p2Sprite.rectTransform.sizeDelta = new Vector2(213.54f, 346.53f);
            }
        }
    }
    public void LoadArena()
    {
        SceneManager.LoadScene("Arena1");
    }

    public void Credits()
    {
        SceneManager.LoadScene("CreditsMenu");
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
