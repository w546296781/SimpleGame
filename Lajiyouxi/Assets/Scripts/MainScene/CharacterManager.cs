using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour
{
    public Text text_life, text_armor, text_atk, text_ap, text_str, text_agi, text_int, text_level, text_point;
    public Text text_speed, text_dodge, text_critChance, text_critDamage, text_fireR, text_coldR, text_lightR, text_fireP, text_coldP, text_lightP;
    public Button btn_save, btn_restore, btn_str, btn_agi, btn_int;

    HeroClass hero;
    // Start is called before the first frame update
    void Start()
    {
        DBManager dbm = new DBManager();
        hero = dbm.GetHero(1);
        Refresh();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Btn_Quit_Click()
    {
        gameObject.transform.parent.GetComponent<MainManager>().ShowUI();
        DestroyImmediate(gameObject);
    }

    public void Refresh()
    {
        text_level.text = hero.level.ToString();
        text_life.text = hero.life.ToString();
        text_armor.text = hero.def.ToString();
        text_atk.text = hero.atk.ToString();
        text_ap.text = hero.ap.ToString();
        text_str.text = hero.str.ToString();
        text_agi.text = hero.agi.ToString();
        text_int.text = hero.Int.ToString();
        text_speed.text = hero.speed.ToString();
        text_dodge.text = hero.dodge.ToString();
        text_critChance.text = hero.critChance.ToString();
        text_critDamage.text = hero.critDamage.ToString();
        text_fireR.text = hero.fireResis.ToString();
        text_coldR.text = hero.coldResis.ToString();
        text_lightR.text = hero.lightResis.ToString();
        text_fireP.text = hero.FirePene.ToString();
        text_coldP.text = hero.coldPene.ToString();
        text_lightP.text = hero.lightPene.ToString();
        text_point.text = hero.attrPoint.ToString();
    }

    public void Btn_STR_Click()
    {

    }
}
