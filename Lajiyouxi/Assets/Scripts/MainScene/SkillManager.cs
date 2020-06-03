using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour
{
    public List<SkillClass> skillList;
    public HeroClass hero;

    public Text text_mainSkill_name, text_mainSkill_level, text_secondarySkill1_name, text_secondareySkill1_level, text_secondarySkill2_name, text_secondareySkill2_level;
    public Text text_SDL_level, text_BD_level, text_HQS_level, text_BFX_level, text_HY_level, text_ATKup_level, text_DEFup_level, text_SPDup_level, text_APup_level;
    public Text text_remainPoint, text_detail_name;

    private SkillClass SDL, BD, HQS, BFX, HY, ATKup, DEFup, SPDup, APup;
    private bool isAdding =  false;
    // Start is called before the first frame update
    void Start()
    {
        DBManager dbm = new DBManager();
        skillList = dbm.GetAllSkill();
        hero = dbm.GetHero(1);

        foreach(SkillClass i in skillList)
        {
            switch (i.id)
            {
                case 1:           //闪电链
                    SDL = i;
                    break;
                case 2:           //冰弹
                    BD = i;
                    break;
                case 3:           //火球术
                    HQS = i;
                    break;
                case 4:           //暴风雪
                    BFX = i;
                    break;
                case 5:           //火雨
                    HY = i;
                    break;
                case 6:           //atkup
                    ATKup = i;
                    break;
                case 7:           //defup
                    DEFup = i;
                    break;
                case 8:           //speedup
                    SPDup = i;
                    break;
                case 9:           //apup
                    APup = i;
                    break;
                default:
                    break;
            }
        }
        Refresh();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Btn_Quit_Click()
    {
        transform.parent.GetComponent<MainManager>().ShowUI();
        DestroyImmediate(gameObject);
    }

    public void Refresh()
    {
        text_remainPoint.text = hero.skillPoint.ToString();

        foreach (SkillClass i in skillList)
        {
            if (i.active == 1)
            {
                text_mainSkill_name.text = i.name;
                text_mainSkill_level.text = i.level.ToString();
            }
            else if (i.active == 2)
            {
                text_secondarySkill1_name.text = i.name;
                text_secondareySkill1_level.text = i.level.ToString();
            }
            else if (i.active == 3)
            {
                text_secondarySkill2_name.text = i.name;
                text_secondareySkill2_level.text = i.level.ToString();
            }

            switch (i.id)
            {
                case 1:           //闪电链
                    text_SDL_level.text = i.level.ToString();
                    break;
                case 2:           //冰弹
                    text_BD_level.text = i.level.ToString();
                    break;
                case 3:           //火球术
                    text_HQS_level.text = i.level.ToString();
                    break;
                case 4:           //暴风雪
                    text_BFX_level.text = i.level.ToString();
                    break;
                case 5:           //火雨
                    text_HY_level.text = i.level.ToString();
                    break;
                case 6:           //atkup
                    text_ATKup_level.text = i.level.ToString();
                    break;
                case 7:           //defup
                    text_DEFup_level.text = i.level.ToString();
                    break;
                case 8:           //speedup
                    text_SPDup_level.text = i.level.ToString();
                    break;
                case 9:           //apup
                    text_APup_level.text = i.level.ToString();
                    break;
                default:
                    break;
            }
        }
    }

    public void Save()
    {
        DBManager dbm = new DBManager();
        dbm.SaveHero(hero);
        foreach(SkillClass i in skillList)
        {
            dbm.SaveSkill(i);
        }
    }

    public void SDL_Click()
    {
        if (isAdding == false)
        {
            text_detail_name.text = SDL.name;
        }
    }



}
