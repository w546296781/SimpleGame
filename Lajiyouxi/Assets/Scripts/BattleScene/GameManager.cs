using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //public TextMesh heroHealth, petHealth, servantHealth, enemy1Health, enemy2Health, enemy3Health, enemy4Health, enemy5Health, enemy6Health;
    public TextMesh resultText, skillNameText;

    private GameObject object_hero, object_pet, object_servant, object_enemy1, object_enemy2, object_enemy3, object_enemy4, object_enemy5, object_enemy6;
    private HeroManager hero, pet, servant, enemy1, enemy2, enemy3, enemy4, enemy5, enemy6;

    private List<GameObject> object_heroTeam, object_enemyTeam;
    private List<HeroManager> heroTeam, enemyTeam;

    public int gameID = 1;
    public int heroSkillCount = 0;

    public HeroClass theHero;

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

        DBManager dbm = new DBManager();

        HeroClass hc = new HeroClass();
        hc = dbm.GetHero(gameID);
        theHero = hc;

        hero.atk = hc.atk;
        hero.def = hc.def;
        hero.speed = hc.speed;
        hero.health = hc.life;
        hero.shownName = hc.name;

        PetClass pc = new PetClass();
        pc = dbm.GetPet(gameID);

        pet.atk = pc.atk;
        pet.def = pc.def;
        pet.speed = pc.speed;
        pet.shownName = pc.name;

        ServantClass sc = new ServantClass();
        sc = dbm.GetServant(gameID);

        servant.atk = sc.atk;
        servant.def = sc.def;
        servant.speed = sc.speed;
        servant.shownName = sc.name;

        List<EnemyClass> enemyList = dbm.GetAllEnemy();

        for(int i = 0; i < enemyTeam.Count; i++)
        {
            EnemyClass ec = enemyList[Random.Range(1, enemyList.Count)];
            enemyTeam[i].atk = ec.atk;
            enemyTeam[i].def = ec.def;
            enemyTeam[i].speed = ec.speed;
            enemyTeam[i].shownName = ec.name;
            enemyTeam[i].health = ec.life;
        }

        pet.isLive = false;
        servant.isLive = false;
        object_pet.SetActive(false);
        object_servant.SetActive(false);
        

        foreach (HeroManager heros in heroTeam)
        {
            float time = 100.0f / heros.speed;
            Invoke("AttackTimer_" + heros.name, time);
        }
        //Invoke("AttackTimer_Hero", 0.9f);

        foreach (HeroManager enemys in enemyTeam)
        {
            float time = 100.0f / enemys.speed;
            Invoke("AttackTimer_" + enemys.name, time);
        }

    }

    // Update is called once per frame
    void Update()
    {
/*        heroHealth.text = hero.health.ToString();
        petHealth.text = pet.health.ToString();
        servantHealth.text = servant.health.ToString();
        enemy1Health.text = enemy1.health.ToString();
        enemy2Health.text = enemy2.health.ToString();
        enemy3Health.text = enemy3.health.ToString();
        enemy4Health.text = enemy4.health.ToString();
        enemy5Health.text = enemy5.health.ToString();
        enemy6Health.text = enemy6.health.ToString();*/

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
            }
            else
            {
                for (int i = 0; i < enemyTeam.Count; i++)
                {
                    if (enemyTeam[i].isLive == true)
                    {
                        Attack(pet, enemyTeam[i]);
                        break;
                    }
                }
            }
            Invoke("AttackTimer_Pet", 100.0f / pet.speed);
        }
    }

    public void AttackTimer_Hero()
    {
        if (hero.isLive == true)
        {
            if (enemy2.isLive == true)
            {
                HeroAttack(enemy2);
            }
            else
            {
                for (int i = 0; i < enemyTeam.Count; i++)
                {
                    if (i >= 3 && enemy5.isLive == true)
                    {
                        HeroAttack(enemy5);
                        break;
                    }
                    else if (enemyTeam[i].isLive == true)
                    {
                        HeroAttack(enemyTeam[i]);
                        break;
                    }
                }
            }
            Invoke("AttackTimer_Hero", 100.0f / hero.speed);
        }
    }

    public void AttackTimer_Servant()
    {
        if (servant.isLive == true)
        {
            if (enemy3.isLive == true)
            {
                Attack(servant, enemy3);
            }
            else
            {
                for (int i = 0; i < enemyTeam.Count; i++)
                {
                    if (i >= 3 && enemy6.isLive == true)
                    {
                        Attack(servant, enemy6);
                        break;
                    }
                    else if (enemyTeam[i].isLive == true)
                    {
                        Attack(pet, enemyTeam[i]);
                        break;
                    }
                }
            }
            Invoke("AttackTimer_Servant", 100.0f / servant.speed);
        }
    }

    public void AttackTimer_Enemy1()
    {
        if (enemy1.isLive == true)
        {
            if (pet.isLive == true)
            {
                Attack(enemy1, pet);
            }
            else
            {
                for (int i = 0; i < heroTeam.Count; i++)
                {
                    if (heroTeam[i].isLive == true)
                    {
                        Attack(enemy1, heroTeam[i]);
                        break;
                    }
                }
            }
            Invoke("AttackTimer_Enemy1", 100.0f / enemy1.speed);
        }
    }

    public void AttackTimer_Enemy2()
    {
        if (enemy2.isLive == true)
        {
            if (hero.isLive == true)
            {
                Attack(enemy2, hero);
            }
            else
            {
                for (int i = 0; i < heroTeam.Count; i++)
                {
                    if (heroTeam[i].isLive == true)
                    {
                        Attack(enemy2, heroTeam[i]);
                        break;
                    }
                }
            }
            Invoke("AttackTimer_Enemy2", 100.0f / enemy2.speed);
        }
    }

    public void AttackTimer_Enemy3()
    {
        if (enemy3.isLive == true)
        {
            if (servant.isLive == true)
            {
                Attack(enemy3, servant);
            }
            else
            {
                for (int i = 0; i < heroTeam.Count; i++)
                {
                    if (heroTeam[i].isLive == true)
                    {
                        Attack(enemy3, heroTeam[i]);
                        break;
                    }
                }
            }
            Invoke("AttackTimer_Enemy3", 100.0f / enemy3.speed);
        }
    }

    public void AttackTimer_Enemy4()
    {
        if (enemy4.isLive == true)
        {
            if (pet.isLive == true)
            {
                Attack(enemy4, pet);
            }
            else
            {
                for (int i = 0; i < heroTeam.Count; i++)
                {
                    if (heroTeam[i].isLive == true)
                    {
                        Attack(enemy4, heroTeam[i]);
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
            }
            else
            {
                for (int i = 0; i < heroTeam.Count; i++)
                {
                    if (heroTeam[i].isLive == true)
                    {
                        Attack(enemy5, heroTeam[i]);
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
            }
            else
            {
                for (int i = 0; i < heroTeam.Count; i++)
                {
                    if (heroTeam[i].isLive == true)
                    {
                        Attack(enemy6, heroTeam[i]);
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

    public void HeroAttack(HeroManager enemy)
    {
        heroSkillCount++;
        if(heroSkillCount % 6 == 0)
        {
            //释放副技能2
            Skill(theHero.skillList[2][0], theHero.skillList[2][1], enemy);
            
        }
        else if(heroSkillCount % 3 == 0)
        {
            //释放副技能1
            Skill(theHero.skillList[1][0], theHero.skillList[1][1], enemy);
        }
        else
        {
            //释放主技能
            Skill(theHero.skillList[0][0], theHero.skillList[0][1], enemy);
        }
    }

    public void Skill(int skillName, int skillLevel, HeroManager enemy)
    {
        int damage = 0;
        int elseTargetCount = 0;
        //基本伤害公式：（基础伤）*1.3技能等级次方*（1+法强/100）
        if (skillName == 1)
        {
            //闪电链：对目标敌人及其他两名敌人造成伤害
            damage = 10;
            for(int i = 0; i < skillLevel; i++)
            {
                damage = (int)(1.3 * damage);
            }
            damage = damage * (1 + theHero.ap / 100);

            elseTargetCount = 2;
        }
        else if(skillName == 2)
        {
            //火球术：对目标敌人造成伤害
            damage = 500;
            for (int i = 0; i < skillLevel; i++)
            {
                damage = (int)(1.3 * damage);
            }
            damage = damage * (1 + theHero.ap / 100);

            elseTargetCount = 0;
        }
        else if(skillName == 3)
        {
            //暴风雪：对全体敌人造成伤害
            damage = 100;
            for (int i = 0; i < skillLevel; i++)
            {
                damage = (int)(1.3 * damage);
            }
            damage = damage * (1 + theHero.ap / 100);

            elseTargetCount = 5;
        }

        Spell(enemy, damage, elseTargetCount);
    }

    public void Spell(HeroManager targetEnemy, int damage, int elseTarget)
    {
        AttackToEnemy(targetEnemy, damage);

        if (elseTarget > 0)
        {
            List<HeroManager> newEnemyList = new List<HeroManager>();
            newEnemyList.Add(enemy1);
            newEnemyList.Add(enemy2);
            newEnemyList.Add(enemy3);
            newEnemyList.Add(enemy4);
            newEnemyList.Add(enemy5);
            newEnemyList.Add(enemy6);

            newEnemyList.Remove(targetEnemy);
            int targetCount = elseTarget;
            while (targetCount > 0)
            {
                if (newEnemyList.Count > 0)
                {
                    HeroManager newEnemy = newEnemyList[Random.Range(0, newEnemyList.Count - 1)];
                    if (newEnemy.isLive == true)
                    {
                        AttackToEnemy(newEnemy, damage);
                        targetCount--;
                    }
                    newEnemyList.Remove(newEnemy);
                }
                else
                {
                    targetCount = 0;
                }
            }
        }

    }

    public void AttackToEnemy(HeroManager enemy, int damage)
    {
        hero.AttackAction();
        damage = damage - enemy.def;
        if (damage > 0)
        {
            enemy.health -= damage;
            enemy.Blink();
            if (enemy.health <= 0)
            {
                enemy.isLive = false;
            }
        }
        Debug.Log(hero.gameObject.name + " Attack " + enemy.gameObject.name + "\nDamage : " + damage);
    }

    public void ShowSkillName()
    {

    }

    public void BackToMain()
    {
        DBManager dbm = new DBManager();
        EventClass theEvent = dbm.GetEvent(1);
        theEvent.battle_finish = 1;
        dbm.SaveEvent(theEvent);
        SceneManager.LoadScene(1);
    }

}
