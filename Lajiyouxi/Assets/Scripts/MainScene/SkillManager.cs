﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour
{
    public List<SkillClass> skillList;
    public HeroClass hero;

    public Text text_mainSkill_name, text_mainSkill_level, text_secondarySkill1_name, text_secondarySkill1_level, text_secondarySkill2_name, text_secondarySkill2_level;
    public Text text_SDL_level, text_BD_level, text_HQS_level, text_BFX_level, text_HY_level, text_ATKup_level, text_DEFup_level, text_SPDup_level, text_APup_level;
    public Text text_remainPoint, text_detail_name, text_detail_intro, text_detail_level, text_detail_damage;

    public Text text_1_1_name, text_1_1_level;
    public Text text_1_2_name, text_1_2_level;
    public Text text_1_3_name, text_1_3_level;
    public Text text_2_1_name, text_2_1_level;
    public Text text_2_2_name, text_2_2_level;
    public Text text_2_3_name, text_2_3_level;
    public Text text_3_1_name, text_3_1_level;
    public Text text_3_2_name, text_3_2_level;
    public Text text_3_3_name, text_3_3_level;
    public Text text_4_1_name, text_4_1_level;
    public Text text_4_2_name, text_4_2_level;
    public Text text_4_3_name, text_4_3_level;

    public Button btn_save, btn_equip, btn_cancel, btn_addPoint, btn_equip2;
    public Text text_equip;


    public GameObject panel;

    private SkillClass SDL, BD, HQS, BFX, HY, ATKup, DEFup, SPDup, APup;
    private bool isAdding =  false;
    private SkillClass selectedSkill;
    // Start is called before the first frame update
    void Start()
    {
        panel.SetActive(false);

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
                case 2:           //火球术
                    HQS = i;
                    break;
                case 3:           //暴风雪
                    BFX = i;
                    break;
                case 4:           //冰弹
                    BD = i;
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

        btn_save.gameObject.SetActive(false);
        btn_cancel.gameObject.SetActive(false);
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
                text_secondarySkill1_level.text = i.level.ToString();
            }
            else if (i.active == 3)
            {
                text_secondarySkill2_name.text = i.name;
                text_secondarySkill2_level.text = i.level.ToString();
            }
        }

        text_SDL_level.text = SDL.level.ToString();
        text_BD_level.text = BD.level.ToString();
        text_HQS_level.text = HQS.level.ToString();
        text_BFX_level.text = BFX.level.ToString();
        text_HY_level.text = HY.level.ToString();
        text_ATKup_level.text = ATKup.level.ToString();
        text_DEFup_level.text = DEFup.level.ToString();
        text_SPDup_level.text = SPDup.level.ToString();
        text_APup_level.text = APup.level.ToString();

    }

    public int GetSkillIDbyName(string skillName)
    {
        int result = 0;
        switch (skillName)
        {
            case "闪电链":           //闪电链
                result = 1;
                break;
            case "火球术":           //火球术
                result = 2;
                break;
            case "暴风雪":           //暴风雪
                result = 3;
                break;
            case "冰弹":           //冰弹
                result = 4;
                break;
            case "火雨":           //火雨
                result = 5;
                break;
            case "攻击力提升":           //atkup
                result = 6;
                break;
            case "防御力提升":           //defup
                result = 7;
                break;
            case "速度提升":           //speedup
                result = 8;
                break;
            case "法强提升":           //apup
                result = 9;
                break;
            default:
                result = 0;
                break;
        }
        return result;
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

    public void SaveSkilltoHero()
    {
        List<List<int>> newSkillList = new List<List<int>>();
        List<int> mainSkill = new List<int>();
        List<int> secondarySkill1 = new List<int>();
        List<int> secondarySkill2 = new List<int>();

        int mainSkill_ID = GetSkillIDbyName(text_mainSkill_name.text);
        int secondarySkill1_ID = GetSkillIDbyName(text_secondarySkill1_name.text);
        int secondarySkill2_ID = GetSkillIDbyName(text_secondarySkill2_name.text);

        foreach(SkillClass i in skillList)
        {
            if(i.id == mainSkill_ID)
            {
                i.active = 1;
            }
            else if(i.id == secondarySkill1_ID)
            {
                i.active = 2;
            }
            else if(i.id == secondarySkill2_ID)
            {
                i.active = 3;
            }
            else
            {
                i.active = 0;
            }
        }

        mainSkill.Add(mainSkill_ID);
        mainSkill.Add(int.Parse(text_mainSkill_level.text));
        secondarySkill1.Add(secondarySkill1_ID);
        secondarySkill1.Add(int.Parse(text_secondarySkill1_level.text));
        secondarySkill2.Add(secondarySkill2_ID);
        secondarySkill2.Add(int.Parse(text_secondarySkill2_level.text));

        newSkillList.Add(mainSkill);
        newSkillList.Add(secondarySkill1);
        newSkillList.Add(secondarySkill2);

        hero.skillList = newSkillList;

        Save();
    }

    public void SDL_Click()
    {
        if (isAdding == false)
        {
            selectedSkill = SDL;

            panel.SetActive(true);
            text_detail_name.text = SDL.name;
            text_detail_intro.text = "对目标敌人及其他两名敌人造成闪电伤害";
            Refresh_Detail(SDL);

            text_1_1_name.text = "闪电链1-1";
            text_1_2_name.text = "闪电链1-2";
            text_1_3_name.text = "闪电链1-3";
            text_2_1_name.text = "闪电链2-1";
            text_2_2_name.text = "闪电链2-2";
            text_2_3_name.text = "闪电链2-3";
            text_3_1_name.text = "闪电链3-1";
            text_3_2_name.text = "闪电链3-2";
            text_3_3_name.text = "闪电链3-3";
            text_4_1_name.text = "闪电链4-1";
            text_4_2_name.text = "闪电链4-2";
            text_4_3_name.text = "闪电链4-3";

            

        }
    }

    public void BD_Click()
    {
        if (isAdding == false)
        {
            selectedSkill = BD;

            panel.SetActive(true);
            text_detail_name.text = BD.name;
            text_detail_intro.text = "对目标敌人造成冰冷伤害";
            Refresh_Detail(BD);

            text_1_1_name.text = "冰弹1-1";
            text_1_2_name.text = "冰弹1-2";
            text_1_3_name.text = "冰弹1-3";
            text_2_1_name.text = "冰弹2-1";
            text_2_2_name.text = "冰弹2-2";
            text_2_3_name.text = "冰弹2-3";
            text_3_1_name.text = "冰弹3-1";
            text_3_2_name.text = "冰弹3-2";
            text_3_3_name.text = "冰弹3-3";
            text_4_1_name.text = "冰弹4-1";
            text_4_2_name.text = "冰弹4-2";
            text_4_3_name.text = "冰弹4-3";


        }
    }

    public void HQS_Click()
    {
        if (isAdding == false)
        {
            selectedSkill = HQS;

            panel.SetActive(true);
            text_detail_name.text = HQS.name;
            text_detail_intro.text = "对目标敌人造成火焰伤害";
            Refresh_Detail(HQS);

            text_1_1_name.text = "火球术1-1";
            text_1_2_name.text = "火球术1-2";
            text_1_3_name.text = "火球术1-3";
            text_2_1_name.text = "火球术2-1";
            text_2_2_name.text = "火球术2-2";
            text_2_3_name.text = "火球术2-3";
            text_3_1_name.text = "火球术3-1";
            text_3_2_name.text = "火球术3-2";
            text_3_3_name.text = "火球术3-3";
            text_4_1_name.text = "火球术4-1";
            text_4_2_name.text = "火球术4-2";
            text_4_3_name.text = "火球术4-3";


        }
    }

    public void BFX_Click()
    {
        if (isAdding == false)
        {
            selectedSkill = BFX;

            panel.SetActive(true);
            text_detail_name.text = BFX.name;
            text_detail_intro.text = "对全体敌人造成冰冷伤害";
            Refresh_Detail(BFX);

            text_1_1_name.text = "暴风雪1-1";
            text_1_2_name.text = "暴风雪1-2";
            text_1_3_name.text = "暴风雪1-3";
            text_2_1_name.text = "暴风雪2-1";
            text_2_2_name.text = "暴风雪2-2";
            text_2_3_name.text = "暴风雪2-3";
            text_3_1_name.text = "暴风雪3-1";
            text_3_2_name.text = "暴风雪3-2";
            text_3_3_name.text = "暴风雪3-3";
            text_4_1_name.text = "暴风雪4-1";
            text_4_2_name.text = "暴风雪4-2";
            text_4_3_name.text = "暴风雪4-3";


        }
    }

    public void HY_Click()
    {
        if (isAdding == false)
        {
            selectedSkill = HY;

            panel.SetActive(true);
            text_detail_name.text = HY.name;
            text_detail_intro.text = "对全体敌人造成火焰伤害";
            Refresh_Detail(HY);

            text_1_1_name.text = "火雨1-1";
            text_1_2_name.text = "火雨1-2";
            text_1_3_name.text = "火雨1-3";
            text_2_1_name.text = "火雨2-1";
            text_2_2_name.text = "火雨2-2";
            text_2_3_name.text = "火雨2-3";
            text_3_1_name.text = "火雨3-1";
            text_3_2_name.text = "火雨3-2";
            text_3_3_name.text = "火雨3-3";
            text_4_1_name.text = "火雨4-1";
            text_4_2_name.text = "火雨4-2";
            text_4_3_name.text = "火雨4-3";

        }
    }

    public void Refresh_Detail(SkillClass skill)
    {
        int damage = skill.basicDamage;
        for (int i = 0; i < skill.level; i++)
        {
            damage = (int)(1.3 * damage);
        }
        damage = damage * (1 + hero.ap / 100);

        text_detail_level.text = "技能等级：" + skill.level;
        text_detail_damage.text = "技能伤害：" + damage;

        text_1_1_level.text = skill.passive1.ToString();
        text_1_2_level.text = skill.passive2.ToString();
        text_1_3_level.text = skill.passive3.ToString();
        text_2_1_level.text = skill.passive4.ToString();
        text_2_2_level.text = skill.passive5.ToString();
        text_2_3_level.text = skill.passive6.ToString();
        text_3_1_level.text = skill.passive7.ToString();
        text_3_2_level.text = skill.passive8.ToString();
        text_3_3_level.text = skill.passive9.ToString();
        text_4_1_level.text = skill.passive10.ToString();
        text_4_2_level.text = skill.passive11.ToString();
        text_4_3_level.text = skill.passive12.ToString();

        if(skill.level <= 0 || skill.type == 3)
        {
            btn_equip.gameObject.SetActive(false);
            btn_equip2.gameObject.SetActive(false);
        }
        else
        {
            btn_equip.gameObject.SetActive(true);
            if(skill.type == 1)
            {
                btn_equip2.gameObject.SetActive(false);
                text_equip.text = "装备";
            }
            else
            {
                btn_equip2.gameObject.SetActive(true);
                text_equip.text = "装备至1";
            }
        }

    }

    public void Btn_Equip_Click()
    {
        if(selectedSkill.type == 1)
        {
            text_mainSkill_level.text = selectedSkill.level.ToString();
            text_mainSkill_name.text = selectedSkill.name;
        }
        else if (selectedSkill.type == 2)
        {
            if (selectedSkill.name == text_secondarySkill2_name.text)
            {

                text_secondarySkill2_level.text = text_secondarySkill1_level.text;
                text_secondarySkill2_name.text = text_secondarySkill1_name.text;
            }

            text_secondarySkill1_level.text = selectedSkill.level.ToString();
            text_secondarySkill1_name.text = selectedSkill.name;
        }

        SaveSkilltoHero();
    }

    public void Btn_Equip2_Click()
    {
        if (text_secondarySkill1_name.text == selectedSkill.name)
        {

            text_secondarySkill1_level.text = text_secondarySkill2_level.text;
            text_secondarySkill1_name.text = text_secondarySkill2_name.text;
        }

        text_secondarySkill2_level.text = selectedSkill.level.ToString();
        text_secondarySkill2_name.text = selectedSkill.name;

        SaveSkilltoHero();
    }

    public void Btn_AddPoint_Click()
    {

    }

    public void Btn_Save_Click()
    {

    }

    public void Btn_Cancel_Click()
    {

    }

}
