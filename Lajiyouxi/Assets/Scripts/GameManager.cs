using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TextMesh heroHealth, enemyHealth;

    private GameObject object_hero, object_pet, object_servant, object_enemy1, object_enemy2, object_enemy3;
    private HeroManager hero, pet, servant, enemy1, enemy2, enemy3;
    // Start is called before the first frame update
    void Start()
    {
        object_hero = GameObject.Find("Hero");
        object_pet = GameObject.Find("Pet");
        object_servant = GameObject.Find("Servant");
        object_enemy1 = GameObject.Find("Enemy1");
        object_enemy2 = GameObject.Find("Enemy2");
        object_enemy3 = GameObject.Find("Enemy3");

        hero = object_hero.transform.GetComponent<HeroManager>();
        pet = object_pet.transform.GetComponent<HeroManager>();
        servant = object_servant.transform.GetComponent<HeroManager>();
        enemy1 = object_enemy3.transform.GetComponent<HeroManager>();
        enemy2 = object_enemy2.transform.GetComponent<HeroManager>();
        enemy3 = object_enemy3.transform.GetComponent<HeroManager>();

        hero.atk = 60;
        hero.def = 10;

        enemy2.atk = 20;
        enemy2.def = 0;

        Invoke("Timer", 2.0f);
    }

    // Update is called once per frame
    void Update()
    {
        heroHealth.text = hero.health.ToString();
        enemyHealth.text = enemy2.health.ToString();
    }

    void Timer()
    {
        if (enemy2.isLive == true)
        {
            attack(hero, enemy2);
            if (enemy2.isLive == false)
            {
                DestroyImmediate(object_enemy2);
            }
            Invoke("Timer", 2.0f);
        }


    }

    public void attack(HeroManager h1, HeroManager h2)
    {
        int damage = h1.atk - h2.def;
        if(damage > 0)
        {
            h2.health -= damage;
            if(h2.health <= 0)
            {
                h2.isLive = false;
            }
        }
    }

}
