using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroManager : MonoBehaviour
{
    public int health, atk, def;
    public bool isLive;
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
    }
}
