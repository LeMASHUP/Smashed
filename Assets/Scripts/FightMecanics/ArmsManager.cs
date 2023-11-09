using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class ArmsManager : MonoBehaviour
{
    public GameObject[] armsObjects;
    private Vector3 initialPos;
    [SerializeField] private GameObject armObject;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            armObject = armsObjects[Random.Range(0, 2)];
            armObject.transform.position = new Vector3(armObject.transform.position.x + 1.54f, armObject.transform.position.y, armObject.transform.position.z);
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            armObject.transform.position = new Vector3(armObject.transform.position.x - 1.54f, armObject.transform.position.y, armObject.transform.position.z);
        }
    }
}
