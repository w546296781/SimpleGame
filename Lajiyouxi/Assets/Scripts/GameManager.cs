using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TextMesh heroHealth, petHealth, servantHealth, enemy1Health, enemy2Health, enemy3Health, enemy4Health, enemy5Health, enemy6Health;
    public TextMesh resultText;

    private GameObject object_hero, object_pet, object_servant, object_enemy1, object_enemy2, object_enemy3, object_enemy4, object_enemy5, object_enemy6;
    private HeroManager hero, pet, servant, enemy1, enemy2, enemy3, enemy4, enemy5, enemy6;

    private List<GameObject> object_heroTeam, object_enemyTeam;
    private List<HeroManager> heroTeam, enemyTeam;

    // Start is called before the first frame update
    void Start()
    {
        object_hero = GameObject.Find("Hero");
        object_pet = GameObject.Find("Pet");
        object_servant = GameObject.Find("Servant");
        object_enemy1 = GameObject.Find("Enemy1");
        object_enemy2 = GameObject.Find("Enemy2");
        object_enemy3 = GameObject.Find("Enemy3");
        object_enemy4 = GameObject.Find("Enemy4");
        object_enemy5 = GameObject.Find("Enemy5");
        object_enemy6 = GameObject.Find("Enemy6");

        hero = object_hero.transform.GetComponent<HeroManager>();
        pet = object_pet.transform.GetComponent<HeroManager>();
        servant = object_servant.transform.GetComponent<HeroManager>();
        enemy1 = object_enemy1.transform.GetComponent<HeroManager>();
        enemy2 = object_enemy2.transform.GetComponent<HeroManager>();
        enemy3 = object_enemy3.transform.GetComponent<HeroManager>();
        enemy4 = object_enemy1.transform.GetComponent<HeroManager>();
        enemy5 = object_enemy2.transform.GetComponent<HeroManager>();
        enemy6 = object_enemy3.transform.GetComponent<HeroManager>();

        object_heroTeam.Add(object_hero);
        object_heroTeam.Add(object_pet);
        object_heroTeam.Add(object_servant);

        object_enemyTeam.Add(object_enemy1);
        object_enemyTeam.Add(object_enemy2);
        object_enemyTeam.Add(object_enemy3);
        object_enemyTeam.Add(object_enemy4);
        object_enemyTeam.Add(object_enemy5);
        object_enemyTeam.Add(object_enemy6);

        heroTeam.Add(hero);
        heroTeam.Add(pet);
        heroTeam.Add(servant);

        enemyTeam.Add(enemy1);
        enemyTeam.Add(enemy2);
        enemyTeam.Add(enemy3);
        enemyTeam.Add(enemy4);
        enemyTeam.Add(enemy5);
        enemyTeam.Add(enemy6);

        hero.atk = 60;
        hero.def = 10;

        pet.atk = 20;
        pet.def = 10;

        servant.atk = 30;
        servant.def = 10;

        foreach(HeroManager enemy in enemyTeam)
        {
            enemy.atk = 20;
            enemy.def = 0;
        }

        Invoke("Attack_Timer", 2.0f);
    }

    // Update is called once per frame
    void Update()
    {
        heroHealth.text = hero.health.ToString();
        petHealth.text = pet.health.ToString();
        servantHealth.text = servant.health.ToString();
        enemy1Health.text = enemy1.health.ToString();
        enemy2Health.text = enemy2.health.ToString();
        enemy3Health.text = enemy3.health.ToString();
        enemy4Health.text = enemy4.health.ToString();
        enemy5Health.text = enemy5.health.ToString();
        enemy6Health.text = enemy6.health.ToString();

        if (enemy1.isLive == false && enemy2.isLive == false && enemy3.isLive == false && enemy4.isLive == false && enemy5.isLive == false && enemy6.isLive == false)
        {
            resultText.text = "You Win!";
        }
        else if (hero.isLive == false && pet.isLive == false && servant.isLive == false)
        {
            resultText.text = "You Lose!";
        }
    }

    void Attack_Timer()
    {
        if (enemy1.isLive == true && pet.isLive == true)
        {
            Attack(pet, enemy1);
            Debug.Log(enemy1.health);
            if (enemy1.isLive == false)
            {
                DestroyImmediate(object_enemy1);
            }
            else
            {
                enemy1.Blink();
            }
        }

        if (enemy2.isLive == true && hero.isLive == true)
        {
            Attack(hero, enemy2);
            if (enemy2.isLive == false)
            {
                DestroyImmediate(object_enemy2);
            }
            else
            {
                enemy2.Blink();
            }
        }

        if (enemy3.isLive == true && servant.isLive == true)
        {
            Attack(servant, enemy3);
            if (enemy3.isLive == false)
            {
                DestroyImmediate(object_enemy3);
            }
            else
            {
                enemy3.Blink();
            }
        }

        if (pet.isLive == true && enemy1.isLive == true)
        {
            Attack(enemy1, pet);
            if (pet.isLive == false)
            {
                DestroyImmediate(object_pet);
            }
            else
            {
                pet.Blink();
            }
        }

        if (hero.isLive == true && enemy2.isLive == true)
        {
            Attack(enemy2, hero);
            if (hero.isLive == false)
            {
                DestroyImmediate(object_hero);
            }
            else
            {
                hero.Blink();
            }
        }

        if (servant.isLive == true && enemy3.isLive == true)
        {
            Attack(enemy3, servant);
            if (servant.isLive == false)
            {
                DestroyImmediate(object_servant);
            }
            else
            {
                servant.Blink();
            }
        }

        Invoke("Attack_Timer", 2.0f);
    }

    public void Attack(HeroManager h1, HeroManager h2)
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
        Debug.Log(h1.gameObject.name + " Attack " + h2.gameObject.name + "\nDamage : " + damage);
    }

}
