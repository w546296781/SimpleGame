using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdventureManager : MonoBehaviour
{
    public GameObject canvas;
    public MainManager mm;

    public Text text_title, text_btnA, text_btnB, text_HD, text_treasure;
    public Button btn_A, btn_B, btn_ok;

    private List<List<string>> eventList = new List<List<string>>();
    private List<string> theAdventure = new List<string>();
    private EventClass theEvent;
    private HeroClass theHero;


    // Start is called before the first frame update
    void Start()
    {
        text_HD.gameObject.SetActive(false);
        text_treasure.gameObject.SetActive(false);
        btn_ok.gameObject.SetActive(false);

        canvas = transform.parent.gameObject;
        mm = canvas.transform.GetComponent<MainManager>();

        DBManager dbm = new DBManager();
        theEvent = dbm.GetEvent(1);
        theHero = dbm.GetHero(1);

        Event_Init();

        ChooseAnEvent();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Button_OK_Click()
    {
        mm.event_finish = true;
        DestroyImmediate(gameObject);
    }

    public void Event_Init()
    {
        string title = "";
        string choiceA = "";
        string choiceB = "";
        string resultAA = "";
        string resultAB = "";
        string resultBA = "";
        string resultBB = "";
        string mark = "";

        for(int i = 0; i < 2; i++)
        {
            List<string> thisEvent = new List<string>();
            switch (i) 
            {
                case 0:
                    title = "你看到半人马正在袭击一个人类村落，你决定：";
                    choiceA = "A.    帮助村落抵御半人马";
                    choiceB = "B.    加入半人马，一起袭击村落";
                    resultAA = "经过一番血战，你成功击退了半人马，村民为了表示感谢，对你奉上了大量的谢礼";           //+金币和经验
                    resultAB = "你浴血奋战，但无奈寡不敌众，只能眼看着村落被半人马烧毁";
                    resultBA = "你和半人马一起将村落劫掠一空，口袋饱饱";                                         //+金币
                    resultBB = "王国正规军赶来支援，一番血战后，你只得落荒而逃，但你感觉自己的技艺有所精进";         //+经验
                    mark = "BRM";
                    break;
                case 1:
                    title = "你偶然经过一座寺庙，里面有一座佛像好像一直在盯着你看，你决定：";
                    choiceA = "A.    佛祖显灵啦（虔诚跪拜）";
                    choiceB = "B.    你瞅啥，给爷爬（摧毁佛像）";
                    resultAA = "不知道为什么，你感觉自己收到了祝福";        //+经验
                    resultAB = "一不小心，钱包掉功德箱里了";               //-金币
                    resultBA = "拆了佛像感觉仍不过瘾，顺手将贡品一扫而空";   //+金币
                    resultBB = "拆了佛像后，你被住持一顿胖揍，最后不仅赔了佛像钱，还搭上不少医疗费";     // -金币
                    mark = "FX";
                    break;
            }
            thisEvent.Add(title);
            thisEvent.Add(choiceA);
            thisEvent.Add(choiceB);
            thisEvent.Add(resultAA);
            thisEvent.Add(resultAB);
            thisEvent.Add(resultBA);
            thisEvent.Add(resultBB);
            thisEvent.Add(mark);
            eventList.Add(thisEvent);
        }

    }

    public void ChooseAnEvent()
    {
        theAdventure = eventList[Random.Range(0, eventList.Count)];
        text_title.text = theAdventure[0];
        text_btnA.text = theAdventure[1];
        text_btnB.text = theAdventure[2];
    }

    public void Select(int a)
    {
        string result = "";
        int gold = 0;
        int exp = 0;
        int b = Random.Range(0, 2);
        if(a == 0 && b == 0) //AA
        {
            result = theAdventure[3];
            switch (theAdventure[7])
            {
                case "BRM":
                    gold = CalculateGoldOrExp(1000);
                    exp = CalculateGoldOrExp(1000);
                    break;
                case "FX":
                    exp = CalculateGoldOrExp(2000);
                    break;
            }
        }
        else if(a == 0 && b == 1) //AB
        {
            result = theAdventure[4];
            switch (theAdventure[7])
            {
                case "BRM":

                    break;
                case "FX":
                    gold = CalculateGoldOrExp(-2000);
                    break;
            }
        }
        else if(a == 1 && b == 0) //BA
        {
            result = theAdventure[5];
            switch (theAdventure[7])
            {
                case "BRM":
                    gold = CalculateGoldOrExp(2000);
                    break;
                case "FX":
                    gold = CalculateGoldOrExp(2000);
                    break;
            }
        }
        else     //BB
        {
            result = theAdventure[6];
            switch (theAdventure[7])
            {
                case "BRM":
                    exp = CalculateGoldOrExp(2000);
                    break;
                case "FX":
                    gold = CalculateGoldOrExp(-2000);
                    break;
            }
        }

        text_title.text = result;
        btn_A.gameObject.SetActive(false);
        btn_B.gameObject.SetActive(false);

        text_HD.gameObject.SetActive(true);
        text_treasure.gameObject.SetActive(true);
        btn_ok.gameObject.SetActive(true);

        text_treasure.text = "金币：    " + gold + "G        经验：    " + exp + "EXP";

        theHero.gold += gold;
        theHero.HeroGetExp(exp);

        DBManager dbm = new DBManager();
        dbm.SaveHero(theHero);
    }

    public int CalculateGoldOrExp(int num)
    {
        double result = num;

        for (int i = 0; i < theEvent.level - 1; i++)
        {
            result = result * 1.5;
        }

        return System.Convert.ToInt32(result);
    }
}
