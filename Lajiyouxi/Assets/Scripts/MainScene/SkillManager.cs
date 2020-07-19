using System.Collections;
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


    public GameObject panel, detail_popup;

    private SkillClass SDL, BD, HQS, BFX, HY, ATKup, DEFup, SPDup, APup;
    private bool isAdding =  false;
    public SkillClass selectedSkill;

    public string selectedDetail;

    public Image img_detail1_1;
    public Image img_detail1_2;
    public Image img_detail1_3;
    public Image img_detail2_1;
    public Image img_detail2_2;
    public Image img_detail2_3;
    public Image img_detail3_1;
    public Image img_detail3_2;
    public Image img_detail3_3;
    public Image img_detail4_1;
    public Image img_detail4_2;
    public Image img_detail4_3;

    public Image img_mainSkill;
    public Image img_secondarySkill1;
    public Image img_secondarySkill2;

    public Sprite sprite_sdl;
    public Sprite sprite_hy;
    public Sprite sprite_bd;
    public Sprite sprite_hqs;
    public Sprite sprite_bfx;
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

        if(hero.skillPoint <= 0)
        {
            btn_addPoint.gameObject.SetActive(false);
        }
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
                img_mainSkill.sprite = GetImageByName(i.name);
            }
            else if (i.active == 2)
            {
                text_secondarySkill1_name.text = i.name;
                text_secondarySkill1_level.text = i.level.ToString();
                img_secondarySkill1.sprite = GetImageByName(i.name);
            }
            else if (i.active == 3)
            {
                text_secondarySkill2_name.text = i.name;
                text_secondarySkill2_level.text = i.level.ToString();
                img_secondarySkill2.sprite = GetImageByName(i.name);
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

    #region DataLayer

    public void SaveSelectedSkill()
    {
        selectedSkill.passive1 = int.Parse(text_1_1_level.text);
        selectedSkill.passive2 = int.Parse(text_1_2_level.text);
        selectedSkill.passive3 = int.Parse(text_1_3_level.text);
        selectedSkill.passive4 = int.Parse(text_2_1_level.text);
        selectedSkill.passive5 = int.Parse(text_2_2_level.text);
        selectedSkill.passive6 = int.Parse(text_2_3_level.text);
        selectedSkill.passive7 = int.Parse(text_3_1_level.text);
        selectedSkill.passive8 = int.Parse(text_3_2_level.text);
        selectedSkill.passive9 = int.Parse(text_3_3_level.text);
        selectedSkill.passive10 = int.Parse(text_4_1_level.text);
        selectedSkill.passive11 = int.Parse(text_4_2_level.text);
        selectedSkill.passive12 = int.Parse(text_4_3_level.text);

        selectedSkill.level = selectedSkill.passive1 + selectedSkill.passive2 + selectedSkill.passive3 + selectedSkill.passive4 + selectedSkill.passive5 + selectedSkill.passive6 +
            selectedSkill.passive7 + selectedSkill.passive8 + selectedSkill.passive9 + selectedSkill.passive10 + selectedSkill.passive11 + selectedSkill.passive12;

        DBManager dbm = new DBManager();
        dbm.SaveSkill(selectedSkill);
        
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
    #endregion

    #region Skill_Select
    public void SDL_Click()
    {
        if (isAdding == false)
        {
            selectedSkill = SDL;

            panel.SetActive(true);
            text_detail_name.text = SDL.name;
            text_detail_intro.text = "对目标敌人及其他两名敌人造成闪电伤害";
            Refresh_Detail();

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

            SetAllDetailImage("闪电链");

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
            Refresh_Detail();

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

            SetAllDetailImage("冰弹");
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
            Refresh_Detail();

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

            SetAllDetailImage("火球术");
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
            Refresh_Detail();

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

            SetAllDetailImage("暴风雪");
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
            Refresh_Detail();

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

            SetAllDetailImage("火雨");
        }
    }

    #endregion

    public void Refresh_Detail()
    {
        int damage = selectedSkill.basicDamage;
        for (int i = 0; i < selectedSkill.level; i++)
        {
            damage = (int)(1.3 * damage);
        }
        damage = damage * (1 + hero.ap / 100);

        text_detail_level.text = "技能等级：" + selectedSkill.level;
        text_detail_damage.text = "技能伤害：" + damage;

        text_1_1_level.text = selectedSkill.passive1.ToString();
        text_1_2_level.text = selectedSkill.passive2.ToString();
        text_1_3_level.text = selectedSkill.passive3.ToString();
        text_2_1_level.text = selectedSkill.passive4.ToString();
        text_2_2_level.text = selectedSkill.passive5.ToString();
        text_2_3_level.text = selectedSkill.passive6.ToString();
        text_3_1_level.text = selectedSkill.passive7.ToString();
        text_3_2_level.text = selectedSkill.passive8.ToString();
        text_3_3_level.text = selectedSkill.passive9.ToString();
        text_4_1_level.text = selectedSkill.passive10.ToString();
        text_4_2_level.text = selectedSkill.passive11.ToString();
        text_4_3_level.text = selectedSkill.passive12.ToString();

        if(selectedSkill.level <= 0 || selectedSkill.type == 3)
        {
            btn_equip.gameObject.SetActive(false);
            btn_equip2.gameObject.SetActive(false);
        }
        else
        {
            btn_equip.gameObject.SetActive(true);
            if(selectedSkill.type == 1)
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

    #region Equip_Skill
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
        Refresh();
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
        Refresh();
    }

    #endregion

    #region Detail_Popup
    public void Detail_Info_Trigger(GameObject obj)
    {
        if (isAdding == false)
        {
            Vector3 createPosition = new Vector3(obj.transform.position.x, obj.transform.position.y + 50 + 35, 10);
            GameObject instance = (GameObject)Instantiate(detail_popup, createPosition, obj.transform.rotation);
            instance.transform.SetParent(panel.transform);

            selectedDetail = obj.name;
        }
    }

    public void Detail_Info_Delete(GameObject obj)
    {
        if (isAdding == false)
        {
            GameObject popup = GameObject.Find("Img_Detail_Popup(Clone)");
            DestroyImmediate(popup);
        }
    }

    #endregion

    public void AddPoint(Text level)
    {
        if (isAdding == true)
        {
            int remainPoint = int.Parse(text_remainPoint.text);
            if (remainPoint > 0)
            {
                text_remainPoint.text = (remainPoint - 1).ToString();
                level.text = (int.Parse(level.text) + 1).ToString();
            }
        }
    }

    public void Btn_AddPoint_Click()
    {
        isAdding = true;
        //让plane边框变色
        btn_save.gameObject.SetActive(true);
        btn_cancel.gameObject.SetActive(true);

        btn_addPoint.gameObject.SetActive(false);
    }

    public void Btn_Save_Click()
    {
        isAdding = false;
        hero.skillPoint = int.Parse(text_remainPoint.text);
        if(hero.skillPoint > 0)
        {
            btn_addPoint.gameObject.SetActive(true);
        }
        btn_save.gameObject.SetActive(false);
        btn_cancel.gameObject.SetActive(false);

        //保存
        hero.skillPoint = int.Parse(text_remainPoint.text);
        SaveSelectedSkill();

        Refresh();
        Refresh_Detail();

        SaveSkilltoHero();
    }

    public void Btn_Cancel_Click()
    {
        isAdding = false;
        btn_addPoint.gameObject.SetActive(true);
        btn_save.gameObject.SetActive(false);
        btn_cancel.gameObject.SetActive(false);

        Refresh();
        Refresh_Detail();
    }

    public Sprite GetImageByName(string name)
    {
        Sprite result = null;

        switch (name)
        {
            case "闪电链":
                result = sprite_sdl;
                break;
            case "冰弹":
                result = sprite_bd;
                break;
            case "火球术":
                result = sprite_hqs;
                break;
            case "暴风雪":
                result = sprite_bfx;
                break;
            case "火雨":
                result = sprite_hy;
                break;
            default:
                break;
        }

        return result;
    }

    public void SetAllDetailImage(string name)
    {
        img_detail1_1.sprite = GetImageByName(name);
        img_detail1_2.sprite = GetImageByName(name);
        img_detail1_3.sprite = GetImageByName(name);
        img_detail2_1.sprite = GetImageByName(name);
        img_detail2_2.sprite = GetImageByName(name);
        img_detail2_3.sprite = GetImageByName(name);
        img_detail3_1.sprite = GetImageByName(name);
        img_detail3_2.sprite = GetImageByName(name);
        img_detail3_3.sprite = GetImageByName(name);
        img_detail4_1.sprite = GetImageByName(name);
        img_detail4_2.sprite = GetImageByName(name);
        img_detail4_3.sprite = GetImageByName(name);
    }

}
