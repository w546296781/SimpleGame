using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleWinManager : MonoBehaviour
{
    public Text text_exp, text_gold, text_equip;
    private HeroClass theHero;
    private EventClass theEvent;

    // Start is called before the first frame update
    void Start()
    {
        DBManager dbm = new DBManager();
        theHero = dbm.GetHero(1);
        theEvent = dbm.GetEvent(1);

        showText();

        Save();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Btn_Confirm_Click()
    {
        transform.parent.GetComponent<MainManager>().ShowUI();
        DestroyImmediate(gameObject);
    }

    public void showText()
    {
        double exp = 100 * 6;
        double gold = 100 * 6;

        for(int i = 0; i < theEvent.level - 1; i++)
        {
            exp = exp * 1.5;
            gold = gold * 1.5;
        }

        text_exp.text = exp.ToString();
        text_gold.text = gold.ToString();

        text_equip.text = "无";

        theHero.exp = theHero.exp - System.Convert.ToInt32(exp);

        if(theHero.exp <= 0)
        {
            theHero.level++;

            double thisLevelExp = 3000;
            for(int i = 0; i < theHero.level - 1; i++)
            {
                thisLevelExp = thisLevelExp * 1.5;
            }

            theHero.exp = System.Convert.ToInt32(thisLevelExp) + theHero.exp;
        }

        theHero.gold = theHero.gold + System.Convert.ToInt32(gold);

    }

    public void Save()
    {
        DBManager dbm = new DBManager();
        dbm.SaveHero(theHero);
    }
}
