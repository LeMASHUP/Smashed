using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class ArmsManager : MonoBehaviour
{
    public GameObject[] armsObjects;
    private Vector3 initialPos;
    [SerializeField] private GameObject armObject;
    [SerializeField] private GameObject bodyObject;
    [SerializeField] private float roty;

    private void Start()
    {
        bodyObject = transform.Find("Body").gameObject;
    }
    void Update()
    {
        roty = bodyObject.transform.eulerAngles.y;
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
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
