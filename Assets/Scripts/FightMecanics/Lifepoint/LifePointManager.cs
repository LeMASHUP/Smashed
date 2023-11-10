using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifePointManager : MonoBehaviour
{
    public int lifePoint = 100;
    [SerializeField] private bool canBeHit = true;

    private void OnCollisionEnter(Collision collision)
    {
        if (canBeHit == true && collision.transform.tag == "Player")
        {
            lifePoint -= 10;
            collision.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.right * 10);
            canBeHit = false;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.GetComponent<ArmsManager>().isPunching == false)
        {
            canBeHit = true;
        }
    }
}
