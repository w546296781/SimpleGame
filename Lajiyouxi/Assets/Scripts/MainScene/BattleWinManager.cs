using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleWinManager : MonoBehaviour
{
    public Text text_exp, text_gold, text_equip, text_packageIsFull;
    private HeroClass theHero;
    private EventClass theEvent;
    private PackageClass thePackage;
    List<EquipmentClass> equipList = new List<EquipmentClass>();
    List<EquipmentClass> dropedList = new List<EquipmentClass>();

    public GameObject item_prefab;

    // Start is called before the first frame update
    void Start()
    {
        DBManager dbm = new DBManager();
        theHero = dbm.GetHero(1);
        theEvent = dbm.GetEvent(1);
        equipList = dbm.GetAllEquipment();
        thePackage = dbm.GetPackage(1);
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

        DropItem();

        theHero.HeroGetExp(exp);

        theHero.gold = theHero.gold + System.Convert.ToInt32(gold);

    }

    public void Save()
    {
        for(int i = 0; i < thePackage.slots.Count; i++)
        {
            if(dropedList.Count == 0)
            {
                break;
            }

            if(thePackage.slots[i] == 0)
            {
                thePackage.slots[i] = dropedList[0].id;
                dropedList.RemoveAt(0);
            }
        }
        if(dropedList.Count != 0)
        {
            text_packageIsFull.text = "背包已满！";
        }

        DBManager dbm = new DBManager();
        dbm.SaveHero(theHero);
        dbm.SavePackage(thePackage);
    }

    public void DropItem()
    {
        int dropCount = 0;

        equipList.Sort(new EquipmentClass());

        foreach(EquipmentClass i in equipList)
        {
            int thresold = 0;
            switch (i.quality)
            {
                case 1:
                    thresold = 20;
                    break;
                case 2:
                    thresold = 15;
                    break;
                case 3:
                    thresold = 10;
                    break;
                case 4:
                    thresold = 5;
                    break;
            }

            if(Random.Range(0,100) < thresold)
            {
                dropCount++;
                dropedList.Add(i);
                PutItem(i, dropCount);
            }

            if(dropCount > 3)
            {
                break;
            }
        }

        if(dropCount == 0)
        {
            text_equip.text = "无";
        }
    }

    public void PutItem(EquipmentClass thisEquip, int count)
    {
        int x = 0;                
        int y = -150;
        switch (count)
        {
            case 1:
                x = -350;
                break;
            case 2:
                x = -250;
                break;
            case 3:
                x = -150;
                break;
        }

        Vector3 thisPosition = new Vector3(x + 960, y + 560, 0);
        GameObject instance = (GameObject)Instantiate(item_prefab, thisPosition, transform.rotation);
        instance.transform.SetParent(transform);
        instance.GetComponent<ItemPrefabManager>().thisEquip = thisEquip;
        instance.GetComponent<ItemPrefabManager>().isOnDropMenu = true;
    }
}
