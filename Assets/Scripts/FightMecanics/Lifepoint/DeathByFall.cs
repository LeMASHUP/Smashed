using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathByFall : MonoBehaviour
{
    [SerializeField] private LifePointManager lifePointManager;

    private void Start()
    {
        lifePointManager = GetComponent<LifePointManager>();
    }
    void Update()
    {
        if (transform.position.y == -14.02)
        {
            lifePointManager.lifePoint = 0;
        }
    }
}
