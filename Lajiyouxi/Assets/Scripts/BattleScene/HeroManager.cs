using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroManager : MonoBehaviour
{
    public string shownName = "";

    public TextMesh text_health;
    public TextMesh text_name;

    public int health = 0, atk, def, speed;
    public bool isLive;

    private float blinkTimer = 0.1f;
    bool blinkTimerOn = false;

    private float attackActionTimer = 0.1f;
    bool attackActionTimerOn = false;

    private Vector3 oldVector;
    // Start is called before the first frame update
    void Start()
    {
        isLive = true;
        oldVector = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        text_name.text = shownName;
        text_health.text = health.ToString();

        if(health <= 0)
        {
            isLive = false;
        }

        if (blinkTimerOn == true)
        {
            blinkTimer -= Time.deltaTime;
            if (blinkTimer <= 0)
            {
                Blink();
                blinkTimer = 0.1f;
            }
        }

        if (attackActionTimerOn == true)
        {
            attackActionTimer -= Time.deltaTime;
            if (attackActionTimer <= 0)
            {
                AttackAction();
                attackActionTimer = 0.1f;
            }
        }
    }

    public void Blink()
    {
        if (gameObject.GetComponent<Renderer>().enabled == true)
        {
            gameObject.GetComponent<Renderer>().enabled = false;
            blinkTimerOn = true;
        }
        else
        {
            gameObject.GetComponent<Renderer>().enabled = true;
            blinkTimerOn = false;
        }
    }

    public void AttackAction()
    {
        if(attackActionTimerOn == false)
        {
            Vector3 newVector;
            if (gameObject.name == "Hero" || gameObject.name == "Pet" || gameObject.name == "Servant")
            {
                newVector = new Vector3(System.Convert.ToSingle(gameObject.transform.position.x.ToString()) + 0.5f, gameObject.transform.position.y, gameObject.transform.position.z);
            }
            else
            {
                newVector = new Vector3(System.Convert.ToSingle(gameObject.transform.position.x.ToString()) - 0.5f, gameObject.transform.position.y, gameObject.transform.position.z);
            }

            gameObject.transform.position = newVector;

            attackActionTimerOn = true;
        }
        else
        {
            gameObject.transform.position = oldVector;
            attackActionTimerOn = false;
        }
    }
}
