using UnityEngine;

public class DeathByFall : MonoBehaviour
{
    [SerializeField] private LifePointManager lifePointManager;

    private void Awake()
    {
        lifePointManager = GetComponent<LifePointManager>();
    }
    void Update()
    {
        if (transform.position.y <= -14.02)
        {
            lifePointManager.lifePoint = 0;
        }
    }
}
