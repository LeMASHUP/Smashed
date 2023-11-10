using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class ArmsManager : MonoBehaviour
{
    public GameObject[] armsObjects;
    public bool isPunching = false;
    private Vector3 initialPos;
    [SerializeField] private GameObject armObject;
    [SerializeField] private GameObject bodyObject;
    [SerializeField] private float roty;
    private LifePointManager lifePointManager;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player" && collision.gameObject != gameObject && isPunching == true)
        {
            lifePointManager = collision.gameObject.GetComponent<LifePointManager>();
            if (lifePointManager.canBeHit == true)
            {
                lifePointManager.lifePoint -= 10;
                if (roty == 180)
                {
                    collision.gameObject.GetComponent<Rigidbody>().AddForce(-250, 500, 0);
                }
                else
                {
                    collision.gameObject.GetComponent<Rigidbody>().AddForce(250, 500, 0);
                }
                lifePointManager.canBeHit = false;
            }
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (isPunching == false && lifePointManager != null)
        {
            lifePointManager.canBeHit = true;
        }
    }
    private void Start()
    {
        bodyObject = transform.Find("Body").gameObject;
    }
    void Update()
    {
        roty = bodyObject.transform.eulerAngles.y;
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isPunching = true;
            armObject = armsObjects[Random.Range(0, 2)];
            if (roty == 180)
            {
                armObject.transform.position = new Vector3(armObject.transform.position.x - 1.54f, armObject.transform.position.y, armObject.transform.position.z);
            }
            else
            {
                armObject.transform.position = new Vector3(armObject.transform.position.x + 1.54f, armObject.transform.position.y, armObject.transform.position.z);
            }
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isPunching = false;
            if (roty == 180)
            {
                armObject.transform.position = new Vector3(armObject.transform.position.x + 1.54f, armObject.transform.position.y, armObject.transform.position.z);
            }
            else
            {
                armObject.transform.position = new Vector3(armObject.transform.position.x - 1.54f, armObject.transform.position.y, armObject.transform.position.z);
            }
        }
    }
}
