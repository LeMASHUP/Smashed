using UnityEngine;
using UnityEngine.SceneManagement;

public class LifePointManager : MonoBehaviour
{
    public int lifePoint = 100;
    public bool canBeHit = true;
    public static bool p1IsDead = false;
    public static bool p2IsDead = false;

    private void Update()
    {
        if (lifePoint <= 0)
        {
            SceneManager.LoadScene("GameOverMenu");
            if (gameObject.name == "Player1(Clone)")
            {
                p1IsDead = true;
            }
            else if (gameObject.name == "Player2(Clone)")
            {
                p2IsDead = true;
            }
        }
    }
}
