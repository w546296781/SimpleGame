using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipManager : MonoBehaviour
{

    List<SlotClass> thisPackage = new List<SlotClass>();
    HeroClass theHero = new HeroClass();
    SlotClass weaponSlot = new SlotClass();
    SlotClass armorSlot = new SlotClass();
    SlotClass halmetSlot = new SlotClass();
    SlotClass bootSlot = new SlotClass();
    SlotClass ringSlot = new SlotClass();
    SlotClass amuletSlot = new SlotClass();

    public Text text_gold;

    public Button btn_sell;

    public bool onSell = false;

    List<EquipmentClass> Equips = new List<EquipmentClass>();
    PackageClass package;

    public GameObject item_prefab;

    // Start is called before the first frame update
    void Start()
    {
        Refresh();
    }

    // Update is called once per frame
    void Update()
    {
    }

    #region Button Function
    public void Btn_Quit_Click()
    {
        gameObject.transform.parent.GetComponent<MainManager>().ShowUI();
        DestroyImmediate(gameObject);
    }

    public void Btn_Sell_Click()
    {
        if(onSell == false)
        {
            onSell = true;
            ColorBlock cb = new ColorBlock();
            cb = btn_sell.colors;
            cb.selectedColor = Color.green;
            cb.normalColor = Color.green;
            cb.highlightedColor = Color.green;
            btn_sell.colors = cb;
        }
        else
        {
            onSell = false;
            ColorBlock cb = new ColorBlock();
            cb = btn_sell.colors;
            cb.selectedColor = Color.white;
            cb.normalColor = Color.white;
            cb.highlightedColor = Color.white;
            btn_sell.colors = cb;
        }
    }

    #endregion

    public void CreatePositions()
    {
        thisPackage.Clear();
        for (int i = 0; i < 30; i++)
        {
            SlotClass newSlot = new SlotClass();
            newSlot.item = package.slots[i];
            newSlot.x = 80 + (i % 6) * 100;
            newSlot.y = 200 - (i / 6) * 100;
            thisPackage.Add(newSlot);
        }

        weaponSlot.item = package.weapon;
        weaponSlot.x = -530;
        weaponSlot.y = 0;

        armorSlot.item = package.armor;
        armorSlot.x = -130;
        armorSlot.y = 200;

        halmetSlot.item = package.halmet;
        halmetSlot.x = -530;
        halmetSlot.y = 200;

        bootSlot.item = package.boot;
        bootSlot.x = -130;
        bootSlot.y = 0;

        ringSlot.item = package.ring;
        ringSlot.x = -530;
        ringSlot.y = -200;

        amuletSlot.item = package.amulet;
        amuletSlot.x = -130;
        amuletSlot.y = -200;

    }

    public void Refresh()
    {


        var objList = GameObject.FindGameObjectsWithTag("item");
        foreach(GameObject i in objList)
        {
            DestroyImmediate(i);
        }
        DestroyImmediate(GameObject.FindGameObjectWithTag("weapon"));
        DestroyImmediate(GameObject.FindGameObjectWithTag("armor"));
        DestroyImmediate(GameObject.FindGameObjectWithTag("halmet"));
        DestroyImmediate(GameObject.FindGameObjectWithTag("boot"));
        DestroyImmediate(GameObject.FindGameObjectWithTag("ring"));
        DestroyImmediate(GameObject.FindGameObjectWithTag("amulet"));

        DBManager dbm = new DBManager();
        Equips = dbm.GetAllEquipment();
        package = dbm.GetPackage(1);
        theHero = dbm.GetHero(1);

        CreatePositions();

        for (int i = 0; i < 30; i++)
        {
            PutItemToSlot(thisPackage[i], i+1);
        }

        PutItemToSlot(weaponSlot, 31);
        PutItemToSlot(armorSlot, 32);
        PutItemToSlot(halmetSlot, 33);
        PutItemToSlot(bootSlot, 34);
        PutItemToSlot(ringSlot, 35);
        PutItemToSlot(amuletSlot, 36);

        text_gold.text = theHero.gold.ToString();
    }

    public void PutItemToSlot(SlotClass thisSlot, int slotID)
    {
        if (thisSlot.item != 0)
        {
            Vector3 thisPosition = new Vector3(thisSlot.x + 960, thisSlot.y + 560, 0);
            GameObject instance = (GameObject)Instantiate(item_prefab, thisPosition, transform.rotation);
            instance.transform.SetParent(transform);

            EquipmentClass thisEquip = new EquipmentClass();
            foreach (EquipmentClass i in Equips)
            {
                if (i.id == thisSlot.item)
                {
                    thisEquip = i;
                }
            }
            instance.GetComponent<ItemPrefabManager>().thisEquip = thisEquip;
            instance.GetComponent<ItemPrefabManager>().slotID = slotID;

            switch (slotID)
            {
                case 31:
                    instance.tag = "weapon";
                    break;
                case 32:
                    instance.tag = "armor";
                    break;
                case 33:
                    instance.tag = "halmet";
                    break;
                case 34:
                    instance.tag = "boot";
                    break;
                case 35:
                    instance.tag = "ring";
                    break;
                case 36:
                    instance.tag = "amulet";
                    break;
                default:
                    break;
            }
        }
    }

    public void SellItem(GameObject obj, int slotID)
    {
        theHero.gold += obj.transform.GetComponent<ItemPrefabManager>().thisEquip.price;
        package.slots[slotID - 1] = 0;
        DBManager dbm = new DBManager();
        dbm.SaveHero(theHero);
        dbm.SavePackage(package);
        DestroyImmediate(obj);
        Refresh();
    }

    public void EquipItem(GameObject obj, int slotID)
    {
        EquipmentClass thisEquip = obj.transform.GetComponent<ItemPrefabManager>().thisEquip;
        if (slotID <= 30)
        {
            SlotClass thisSlot = null;
            switch (thisEquip.Class)
            {
                case 1:
                    thisSlot = weaponSlot;
                    package.weapon = thisEquip.id;
                    break;
                case 2:
                    thisSlot = armorSlot;
                    package.armor = thisEquip.id;
                    break;
                case 3:
                    thisSlot = halmetSlot;
                    package.halmet = thisEquip.id;
                    break;
                case 4:
                    thisSlot = bootSlot;
                    package.boot = thisEquip.id;
                    break;
                case 5:
                    thisSlot = ringSlot;
                    package.ring = thisEquip.id;
                    break;
                case 6:
                    thisSlot = amuletSlot;
                    package.amulet = thisEquip.id;
                    break;
            }

            package.slots[slotID - 1] = thisSlot.item;

            EquipmentClass thatEquip = null;
            foreach(var i in Equips)
            {
                if(i.id == thisSlot.item)
                {
                    thatEquip = i;
                }
            }

            DBManager dbm = new DBManager();
            dbm.SavePackage(package);

            Refresh();

            DropThisEquipToHero(thatEquip);
            AddThisEquipToHero(thisEquip);

        }
        else
        {
            bool packageIsFull = true;
            for(int i = 0; i < package.slots.Count; i++)
            {
                if(package.slots[i] == 0)
                {
                    packageIsFull = false;
                    package.slots[i] = thisEquip.id;
                    break;
                }
            }

            if(packageIsFull == false)
            {
                switch (slotID)
                {
                    case 31:
                        package.weapon = 0;
                        break;
                    case 32:
                        package.armor = 0;
                        break;
                    case 33:
                        package.halmet = 0;
                        break;
                    case 34:
                        package.boot = 0;
                        break;
                    case 35:
                        package.ring = 0;
                        break;
                    case 36:
                        package.amulet = 0;
                        break;
                }

                DBManager dbm = new DBManager();
                dbm.SavePackage(package);
                Refresh();
                DropThisEquipToHero(thisEquip);
            }
        }
    }

    #region Attr

    public void AddThisEquipToHero(EquipmentClass thisEquip)
    {
        if (thisEquip != null)
        {
            AddThisAttr(thisEquip.attr1);
            AddThisAttr(thisEquip.attr2);
            AddThisAttr(thisEquip.attr3);
            AddThisAttr(thisEquip.attr4);
            AddThisAttr(thisEquip.attr5);
            AddThisAttr(thisEquip.attr6);

            DBManager dbm = new DBManager();
            dbm.SaveHero(theHero);
        }
    }

    public void DropThisEquipToHero(EquipmentClass thisEquip)
    {
        if (thisEquip != null)
        {
            DropThisAttr(thisEquip.attr1);
            DropThisAttr(thisEquip.attr2);
            DropThisAttr(thisEquip.attr3);
            DropThisAttr(thisEquip.attr4);
            DropThisAttr(thisEquip.attr5);
            DropThisAttr(thisEquip.attr6);

            DBManager dbm = new DBManager();
            dbm.SaveHero(theHero);
        }
    }

    public void AddThisAttr(string str)
    {
        if(str != "0-0")
        {
            var strList = str.Split('-');
            string name = strList[0];
            int value = int.Parse(strList[1]);
            switch (name)
            {
                case "1":
                    theHero.str += value;
                    theHero.life += 10 * value;
                    theHero.atk += 5 * value;
                    break;         
                case "2":
                    theHero.agi += value;
                    theHero.dodge += 0.1 * value;
                    theHero.speed += 5 * value;
                    break;         
                case "3":
                    theHero.Int += value;
                    theHero.ap += 5 * value;
                    theHero.critChance += 0.5 * value;
                    break;         
                case "4":
                    theHero.atk += value;
                    break;         
                case "5":
                    theHero.def += value;
                    break;
            }
        }
    }

    public void DropThisAttr(string str)
    {
        if (str != "0-0")
        {
            var strList = str.Split('-');
            string name = strList[0];
            int value = int.Parse(strList[1]);
            switch (name)
            {
                case "1":
                    theHero.str -= value;
                    theHero.life -= 10 * value;
                    theHero.atk -= 5 * value;
                    break;
                case "2":
                    theHero.agi -= value;
                    theHero.dodge -= 0.1 * value;
                    theHero.speed -= 5 * value;
                    break;
                case "3":
                    theHero.Int -= value;
                    theHero.ap -= 5 * value;
                    theHero.critChance -= 0.5 * value;
                    break;
                case "4":
                    theHero.atk -= value;
                    break;
                case "5":
                    theHero.def -= value;
                    break;
            }
        }
    }

    #endregion
}
