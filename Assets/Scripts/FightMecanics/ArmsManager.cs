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
    [SerializeField] private bool isCombo = false;
    [SerializeField] private bool canUppercut = true;
    [SerializeField]  private int comboMultiplier = 1;
    private int smallPunchDamage = 1;
    private int bigPunchDamage = 3;
    private LifePointManager lifePointManager;
    private Rigidbody enemyRB;
    private string statePunch = null;
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
            statePunch = "SmallPunch";
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

        if (canUppercut == true)
        {
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                isPunching = true;
                statePunch = "Uppercut";
                armObject = armsObjects[Random.Range(0, 2)];
                if (roty == 180)
                {
                    armObject.transform.position = new Vector3(armObject.transform.position.x - 1.1f, armObject.transform.position.y + 1.54f, armObject.transform.position.z);
                    armObject.transform.eulerAngles = new Vector3(0, 0, 90);
                    gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    gameObject.GetComponent<Rigidbody>().AddForce(0, 250, 0);
                }
                else
                {
                    armObject.transform.position = new Vector3(armObject.transform.position.x + 1.1f, armObject.transform.position.y + 1.54f, armObject.transform.position.z);
                    armObject.transform.eulerAngles = new Vector3(0, 0, 90);
                    gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    gameObject.GetComponent<Rigidbody>().AddForce(0, 250, 0);
                }
            }

            if (Input.GetKeyUp(KeyCode.LeftControl))
            {
                if (roty == 180)
                {
                    armObject.transform.position = new Vector3(armObject.transform.position.x + 1.1f, armObject.transform.position.y - 1.54f, armObject.transform.position.z);
                    armObject.transform.eulerAngles = new Vector3(0, 0, 0);
                    canUppercut = false;
                    Invoke("ResetUppercut", 3);
                }
                else
                {
                    armObject.transform.position = new Vector3(armObject.transform.position.x - 1.1f, armObject.transform.position.y - 1.54f, armObject.transform.position.z);
                    armObject.transform.eulerAngles = new Vector3(0, 0, 0);
                    canUppercut = false;
                    Invoke("ResetUppercut", 3);
                }
            }
        }

        if (isCombo == false)
        {
            if (comboMultiplier > 1)
            {
                Invoke("ResetCombo", 1.5f);
            }
        }
        else
        {
            CancelInvoke("ResetCombo");
        }
    }

    private void ResetCombo()
    {
        comboMultiplier = 1;
    }

    private void ResetUppercut()
    {
        canUppercut = true;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player" && collision.gameObject != gameObject && isPunching == true)
        {
            lifePointManager = collision.gameObject.GetComponent<LifePointManager>();
            enemyRB = collision.gameObject.GetComponent<Rigidbody>();
            enemyRB.velocity = Vector3.zero;
            if (statePunch == "SmallPunch")
            {
                SmallPunch();
            }
            if (statePunch == "Uppercut")
            {
                Uppercut();
            }
        }
    }

    private void Uppercut()
    {
        if (lifePointManager.canBeHit == true)
        {
            lifePointManager.lifePoint -= (bigPunchDamage * comboMultiplier);
            comboMultiplier += 1;
            isCombo = true;
            enemyRB.AddForce(0, 500, 0);
            lifePointManager.canBeHit = false;
        }
    }

    private void SmallPunch()
    {
        if (lifePointManager.canBeHit == true)
        {
            lifePointManager.lifePoint -= (smallPunchDamage * comboMultiplier);
            comboMultiplier += 1;
            isCombo = true;
            if (roty == 180)
            {
                enemyRB.AddForce(-250, 500, 0);
            }
            else
            {
                enemyRB.AddForce(250, 500, 0);
            }
            lifePointManager.canBeHit = false;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (isPunching == false && lifePointManager != null)
        {
            lifePointManager.canBeHit = true;
            isCombo = false;
        }
    }
}
