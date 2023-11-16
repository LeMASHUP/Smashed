using UnityEngine;
using UnityEngine.InputSystem;

public class ArmsManager : MonoBehaviour
{
    public GameObject[] armsObjects;
    public bool isPunching = false;
    public float punchHold = 0.5f;
    public float uppercutHold = 0.5f;
    [SerializeField] private int resetTimer = 3;
    private Vector3 initialPos;
    [SerializeField] private GameObject armObject;
    [SerializeField] private GameObject bodyObject;
    private float roty;
    [SerializeField] private bool isCombo = false;
    public bool isUppercut = false;
    public bool isLowPunch = false;
    [SerializeField] private bool canUppercut = true;
    [SerializeField]  private int comboMultiplier = 1;
    private int smallPunchDamage = 1;
    private int bigPunchDamage = 3;
    [SerializeField] private LifePointManager lifePointManager;
    private Rigidbody enemyRB;
    private string statePunch = null;
    private void Start()
    {
        bodyObject = transform.Find("Body").gameObject;
    }
    void Update()
    {
        roty = bodyObject.transform.eulerAngles.y;

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

    public void SmallPunchPlayer(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (!isUppercut)
            {
                Punching();
                Invoke("StopPunching", punchHold);
            }
        }
        else if (context.canceled)
        {
            CancelInvoke("StopPunching");
            if (isLowPunch)
            {
                StopPunching();
            }
        }
    }

    public void UppercutPlayer(InputAction.CallbackContext context)
    {

        if (canUppercut == true)
        {
            if (context.started)
            {
                if (!isLowPunch)
                {
                    StartUppercut();
                    Invoke("StopUppercut", uppercutHold);
                }

            }

            if (context.canceled)
            {
                CancelInvoke("StopUppercut");
                if (isUppercut)
                {
                    StopUppercut();
                }
            }
        }
    }

    private void Punching()
    {
        isPunching = true;
        isLowPunch = true;
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

    private void StopPunching()
    {
        isPunching = false;
        if (roty == 180)
        {
            armObject.transform.position = new Vector3(armObject.transform.position.x + 1.54f, armObject.transform.position.y, armObject.transform.position.z);
            isLowPunch = false;
        }
        else
        {
            armObject.transform.position = new Vector3(armObject.transform.position.x - 1.54f, armObject.transform.position.y, armObject.transform.position.z);
            isLowPunch = false;
        }
    }

    private void StartUppercut()
    {
        isUppercut = true;
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

    private void StopUppercut()
    {
        if (roty == 180)
        {
            armObject.transform.position = new Vector3(armObject.transform.position.x + 1.1f, armObject.transform.position.y - 1.54f, armObject.transform.position.z);
            armObject.transform.eulerAngles = new Vector3(0, 0, 0);
            canUppercut = false;
            isUppercut = false;
            Invoke("ResetUppercut", resetTimer);
        }
        else
        {
            armObject.transform.position = new Vector3(armObject.transform.position.x - 1.1f, armObject.transform.position.y - 1.54f, armObject.transform.position.z);
            armObject.transform.eulerAngles = new Vector3(0, 0, 0);
            canUppercut = false;
            isUppercut = false;
            Invoke("ResetUppercut", resetTimer);
        }
    }



    private void ResetCombo()
    {
        comboMultiplier = 1;
    }

    private void ResetUppercut()
    {
        statePunch = null;
        canUppercut = true;
    }

    private void Uppercut()
    {
        if (lifePointManager.canBeHit == true)
        {
            lifePointManager.lifePoint -= (bigPunchDamage * comboMultiplier);
            comboMultiplier += 1;
            isCombo = true;
            enemyRB.velocity = Vector3.zero;
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
                enemyRB.AddForce(-100, 250, 0);
            }
            else
            {
                enemyRB.AddForce(100, 250, 0);
            }
            lifePointManager.canBeHit = false;
        }
    }
    private void OnCollisionEnter(Collision collision)
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

    private void OnCollisionExit(Collision collision)
    {
        if (isPunching == false && lifePointManager != null)
        {
            lifePointManager.canBeHit = true;
            isCombo = false;
        }
    }
}