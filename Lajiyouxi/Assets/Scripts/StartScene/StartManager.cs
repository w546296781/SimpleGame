using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartManager : MonoBehaviour
{
    public Button btn_continue, btn_start, btn_quit;
    public GameObject createGameNotice_popup;

    private string savedGame = "";
    // Start is called before the first frame update
    void Start()
    {
        DBManager dbm = new DBManager();
        savedGame = dbm.GetGame(1);
        if(savedGame == "")
        {
            btn_continue.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Btn_Start_Onclick()
    {

        if(savedGame == "")
        {
            CreateNewGame();
        }
        else
        {
            GameObject instance = (GameObject)Instantiate(createGameNotice_popup, new Vector2(960,540), transform.rotation);
            instance.transform.SetParent(transform);
            HideUI();
        }
    }

    public void CreateNewGame()
    {
        DBManager dbm = new DBManager();

        string newGame = System.DateTime.Now.ToString();
        dbm.SaveGame(1, newGame);

        CreateNewEvent();
        CreateNewHero();
        CreateNewPackage();
        CreateNewSkill();

        SceneManager.LoadScene(1);
    }

    public void Btn_Quit_Onclick()
    {
        Application.Quit();
    }

    public void Btn_Continue_Onclick()
    {
        SceneManager.LoadScene(1);
    }

    public void HideUI()
    {
        btn_start.gameObject.SetActive(false);
        btn_quit.gameObject.SetActive(false);
        btn_continue.gameObject.SetActive(false);
    }

    public void ShowUI()
    {
        btn_start.gameObject.SetActive(true);
        btn_quit.gameObject.SetActive(true);
        btn_continue.gameObject.SetActive(true);
    }

    public void CreateNewEvent()
    {
        DBManager dbm = new DBManager();
        EventClass newEvent = new EventClass();
        newEvent.id = 1;
        newEvent.level = 1;
        newEvent.area1 = 0;
        newEvent.area2 = 0;
        newEvent.area3 = 0;
        newEvent.event_left = 30;
        newEvent.battle_position = 0;
        newEvent.battle_finish = 0;
        dbm.SaveEvent(newEvent);
    }

    public void CreateNewHero()
    {
        DBManager dbm = new DBManager();
        HeroClass newHero = new HeroClass();
        newHero.agi = 10;
        newHero.ap = 10;
        newHero.atk = 100;
        newHero.attrPoint = 0;
        newHero.coldPene = 0;
        newHero.coldResis = 0;
        newHero.critChance = 5;
        newHero.critDamage = 200;
        newHero.def = 10;
        newHero.dodge = 0;
        newHero.exp = 3000;
        newHero.FirePene = 0;
        newHero.fireResis = 0;
        newHero.gold = 0;
        newHero.id = 1;
        newHero.Int = 10;
        newHero.level = 1;
        newHero.life = 500;
        newHero.lightPene = 0;
        newHero.lightResis = 0;
        newHero.name = "龙傲天";
        List<List<int>> newSkillList = new List<List<int>>();
        List<int> newSkill1 = new List<int>();
        List<int> newSkill2 = new List<int>();
        List<int> newSkill3 = new List<int>();

        newSkill1.Add(1);
        newSkill1.Add(1);
        newSkill2.Add(2);
        newSkill2.Add(1);
        newSkill3.Add(3);
        newSkill3.Add(1);

        newSkillList.Add(newSkill1);
        newSkillList.Add(newSkill2);
        newSkillList.Add(newSkill3);

        newHero.skillList = newSkillList;
        newHero.skillPoint = 0;
        newHero.speed = 100;
        newHero.str = 10;
        dbm.SaveHero(newHero);
    }

    public void CreateNewPackage()
    {
        DBManager dbm = new DBManager();
        PackageClass newPackage = dbm.GetPackage(1);
        newPackage.amulet = 0;
        newPackage.armor = 0;
        newPackage.boot = 0;
        newPackage.halmet = 0;
        newPackage.ring = 0;
        newPackage.weapon = 0;
        for(int i = 0; i < newPackage.slots.Count; i++)
        {
            newPackage.slots[i] = 0;
        }
        dbm.SavePackage(newPackage);
    }

    public void CreateNewSkill()
    {
        DBManager dbm = new DBManager();
        List<SkillClass> skillList = dbm.GetAllSkill();
        foreach(SkillClass i in skillList)
        {
            i.level = 0;
            i.active = 0;
            i.passive1 = 0;
            i.passive2 = 0;
            i.passive3 = 0;
            i.passive4 = 0;
            i.passive5 = 0;
            i.passive6 = 0;
            i.passive7 = 0;
            i.passive8 = 0;
            i.passive9 = 0;
            i.passive10 = 0;
            i.passive11 = 0;
            i.passive12 = 0;
            if(i.id == 1 || i.id == 2 || i.id == 3)
            {
                i.level = 1;
                i.active = i.id;
                i.passive1 = 1;
            }
            dbm.SaveSkill(i);
        }
    }
}
