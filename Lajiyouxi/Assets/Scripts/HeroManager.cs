using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroManager : MonoBehaviour
{
    public int health, atk, def, speed;
    public bool isLive;

    private float blinkTimer = 0.2f;
    bool blinkTimerOn = false;
    // Start is called before the first frame update
    void Start()
    {
        health = 100;
        isLive = true;
    }

    // Update is called once per frame
    void Update()
    {
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
}
