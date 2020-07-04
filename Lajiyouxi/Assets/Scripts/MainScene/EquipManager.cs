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

}
