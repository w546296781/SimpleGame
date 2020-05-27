﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        enemy4 = object_enemy4.transform.GetComponent<HeroManager>();
        enemy5 = object_enemy5.transform.GetComponent<HeroManager>();
        enemy6 = object_enemy6.transform.GetComponent<HeroManager>();

        object_heroTeam = new List<GameObject>();
        object_enemyTeam = new List<GameObject>();
        heroTeam = new List<HeroManager>();
        enemyTeam = new List<HeroManager>();

        object_heroTeam.Add(object_pet);
        object_heroTeam.Add(object_hero);
        object_heroTeam.Add(object_servant);

        object_enemyTeam.Add(object_enemy1);
        object_enemyTeam.Add(object_enemy2);
        object_enemyTeam.Add(object_enemy3);
        object_enemyTeam.Add(object_enemy4);
        object_enemyTeam.Add(object_enemy5);
        object_enemyTeam.Add(object_enemy6);

        heroTeam.Add(pet);
        heroTeam.Add(hero);
        heroTeam.Add(servant);

        enemyTeam.Add(enemy1);
        enemyTeam.Add(enemy2);
        enemyTeam.Add(enemy3);
        enemyTeam.Add(enemy4);
        enemyTeam.Add(enemy5);
        enemyTeam.Add(enemy6);

        hero.atk = 60;
        hero.def = 10;
        hero.speed = 100;

        pet.atk = 20;
        pet.def = 10;
        pet.speed = 50;

        servant.atk = 30;
        servant.def = 10;
        servant.speed = 70;

        foreach(HeroManager enemy in enemyTeam)
        {
            enemy.atk = 20;
            enemy.def = 0;
        }

        enemy1.speed = 35;
        enemy2.speed = 50;
        enemy3.speed = 40;
        enemy4.speed = 35;
        enemy5.speed = 45;
        enemy6.speed = 50;

        Debug.Log("Here!");

        foreach(HeroManager heros in heroTeam)
        {
            Invoke("AttackTimer_" + heros.name, 100 / heros.speed);
        }

        foreach(HeroManager enemys in enemyTeam)
        {
            Invoke("AttackTimer_" + enemys.name, 100 / enemys.speed);
        }

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
            Invoke("BackToMain", 2.0f);
        }
        else if (hero.isLive == false && pet.isLive == false && servant.isLive == false)
        {
            resultText.text = "You Lose!";
            Invoke("BackToMain", 2.0f);
        }
    }

    public void AttackTimer_Pet()
    {
        if (pet.isLive == true)
        {
            if (enemy1.isLive == true)
            {
                Attack(pet, enemy1);
                if (enemy1.isLive == false)
                {
                    DestroyImmediate(object_enemy1);
                }
            }
            else
            {
                for (int i = 0; i < enemyTeam.Count; i++)
                {
                    if (enemyTeam[i].isLive == true)
                    {
                        Attack(pet, enemyTeam[i]);
                        if (enemyTeam[i].isLive == false)
                        {
                            DestroyImmediate(object_enemyTeam[i]);
                        }
                        break;
                    }
                }
            }
            Invoke("AttackTimer_Pet", 100 / pet.speed);
        }
    }

    public void AttackTimer_Hero()
    {
        if (hero.isLive == true)
        {
            if (enemy2.isLive == true)
            {
                Attack(hero, enemy2);
                if (enemy2.isLive == false)
                {
                    DestroyImmediate(object_enemy2);
                }
            }
            else
            {
                for (int i = 0; i < enemyTeam.Count; i++)
                {
                    if (i >= 3 && enemy5.isLive == true)
                    {
                        Attack(hero, enemy5);
                        if (enemy5.isLive == false)
                        {
                            DestroyImmediate(object_enemy5);
                        }
                        break;
                    }
                    else if (enemyTeam[i].isLive == true)
                    {
                        Attack(hero, enemyTeam[i]);
                        if (enemyTeam[i].isLive == false)
                        {
                            DestroyImmediate(object_enemyTeam[i]);
                        }
                        break;
                    }
                }
            }
            Invoke("AttackTimer_Hero", 100 / hero.speed);
        }
    }

    public void AttackTimer_Servant()
    {
        if (servant.isLive == true)
        {
            if (enemy3.isLive == true)
            {
                Attack(servant, enemy3);
                if (enemy3.isLive == false)
                {
                    DestroyImmediate(object_enemy3);
                }
            }
            else
            {
                for (int i = 0; i < enemyTeam.Count; i++)
                {
                    if (i >= 3 && enemy6.isLive == true)
                    {
                        Attack(servant, enemy6);
                        if (enemy6.isLive == false)
                        {
                            DestroyImmediate(object_enemy6);
                        }
                        break;
                    }
                    else if (enemyTeam[i].isLive == true)
                    {
                        Attack(pet, enemyTeam[i]);
                        if (enemyTeam[i].isLive == false)
                        {
                            DestroyImmediate(object_enemyTeam[i]);
                        }
                        break;
                    }
                }
            }
            Invoke("AttackTimer_Servant", 100 / servant.speed);
        }
    }

    public void AttackTimer_Enemy1()
    {
        if (enemy1.isLive == true)
        {
            if (pet.isLive == true)
            {
                Attack(enemy1, pet);
                if (pet.isLive == false)
                {
                    DestroyImmediate(object_pet);
                }
            }
            else
            {
                for (int i = 0; i < heroTeam.Count; i++)
                {
                    if (heroTeam[i].isLive == true)
                    {
                        Attack(enemy1, heroTeam[i]);
                        if (heroTeam[i].isLive == false)
                        {
                            DestroyImmediate(object_heroTeam[i]);
                        }
                        break;
                    }
                }
            }
            Invoke("AttackTimer_Enemy1", 100 / enemy1.speed);
        }
    }

    public void AttackTimer_Enemy2()
    {
        if (enemy2.isLive == true)
        {
            if (hero.isLive == true)
            {
                Attack(enemy2, hero);
                if (hero.isLive == false)
                {
                    DestroyImmediate(object_hero);
                }
            }
            else
            {
                for (int i = 0; i < heroTeam.Count; i++)
                {
                    if (heroTeam[i].isLive == true)
                    {
                        Attack(enemy2, heroTeam[i]);
                        if (heroTeam[i].isLive == false)
                        {
                            DestroyImmediate(object_heroTeam[i]);
                        }
                        break;
                    }
                }
            }
            Invoke("AttackTimer_Enemy2", 100 / enemy2.speed);
        }
    }

    public void AttackTimer_Enemy3()
    {
        if (enemy3.isLive == true)
        {
            if (servant.isLive == true)
            {
                Attack(enemy3, servant);
                if (servant.isLive == false)
                {
                    DestroyImmediate(object_servant);
                }
            }
            else
            {
                for (int i = 0; i < heroTeam.Count; i++)
                {
                    if (heroTeam[i].isLive == true)
                    {
                        Attack(enemy3, heroTeam[i]);
                        if (heroTeam[i].isLive == false)
                        {
                            DestroyImmediate(object_heroTeam[i]);
                        }
                        break;
                    }
                }
            }
            Invoke("AttackTimer_Enemy3", 100 / enemy3.speed);
        }
    }

    public void AttackTimer_Enemy4()
    {
        if (enemy4.isLive == true)
        {
            if (pet.isLive == true)
            {
                Attack(enemy4, pet);
                if (pet.isLive == false)
                {
                    DestroyImmediate(object_pet);
                }
            }
            else
            {
                for (int i = 0; i < heroTeam.Count; i++)
                {
                    if (heroTeam[i].isLive == true)
                    {
                        Attack(enemy4, heroTeam[i]);
                        if (heroTeam[i].isLive == false)
                        {
                            DestroyImmediate(object_heroTeam[i]);
                        }
                        break;
                    }
                }
            }
            Invoke("AttackTimer_Enemy4", 100 / enemy4.speed);
        }
    }

    public void AttackTimer_Enemy5()
    {
        if (enemy5.isLive == true)
        {
            if (hero.isLive == true)
            {
                Attack(enemy5, hero);
                if (hero.isLive == false)
                {
                    DestroyImmediate(object_hero);
                }
            }
            else
            {
                for (int i = 0; i < heroTeam.Count; i++)
                {
                    if (heroTeam[i].isLive == true)
                    {
                        Attack(enemy5, heroTeam[i]);
                        if (heroTeam[i].isLive == false)
                        {
                            DestroyImmediate(object_heroTeam[i]);
                        }
                        break;
                    }
                }
            }
            Invoke("AttackTimer_Enemy5", 100 / enemy5.speed);
        }
    }

    public void AttackTimer_Enemy6()
    {
        if (enemy6.isLive == true)
        {
            if (servant.isLive == true)
            {
                Attack(enemy6, servant);
                if (servant.isLive == false)
                {
                    DestroyImmediate(object_servant);
                }
            }
            else
            {
                for (int i = 0; i < heroTeam.Count; i++)
                {
                    if (heroTeam[i].isLive == true)
                    {
                        Attack(enemy6, heroTeam[i]);
                        if (heroTeam[i].isLive == false)
                        {
                            DestroyImmediate(object_heroTeam[i]);
                        }
                        break;
                    }
                }
            }
            Invoke("AttackTimer_Enemy6", 100 / enemy6.speed);
        }
    }

    public void Attack(HeroManager h1, HeroManager h2)
    {
        h1.AttackAction();
        int damage = h1.atk - h2.def;
        if(damage > 0)
        {
            h2.health -= damage;
            h2.Blink();
            if(h2.health <= 0)
            {
                h2.isLive = false;
            }
        }
        Debug.Log(h1.gameObject.name + " Attack " + h2.gameObject.name + "\nDamage : " + damage);
    }

    public void BackToMain()
    {
        SceneManager.LoadScene(1);
    }

}